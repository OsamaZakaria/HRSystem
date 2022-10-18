import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { CreateEmployee } from '@app/_models/CreateEmployee';
import { Department } from '@app/_models/department';
import { EmployeeById } from '@app/_models/employeeById';
import { ServerErrors } from '@app/_models/error';
import { Manager } from '@app/_models/manager';
import { DepartmentService } from '@app/_services/department.service';
import { EmployeeService } from '@app/_services/employee.service';
import { Subject, Subscription } from 'rxjs';
import { takeUntil } from 'rxjs/operators';



@Component({
  templateUrl: 'edit-employee.component.html'
})
export class EditEmployeeComponent implements OnInit{
    
  private routeSubscription: Subscription;
  protected ngUnsubscribe: Subject<void> = new Subject<void>();
  public employee = new EmployeeById();
  public managers:Manager[];
  public departments:Department[];
  private employeeId: string;
  errors: ServerErrors;
  constructor(injector: Injector, private employeeService: EmployeeService, private departmentService: DepartmentService,
    private snackBar:MatSnackBar, private router:Router,
    private activatedRoute:ActivatedRoute) {
  }
    ngOnInit(): void {
        this.GetManagers();
        this.GetDepartments();
        this.GetEmployeeById();
    }
    private GetManagers() {
        this.employeeService.getManagers('').subscribe((result) => {
            this.managers = result;
        });
    }
    private GetDepartments() {
        this.departmentService.getAll().subscribe((result) => {
            this.departments = result;
        });
    }
    private GetEmployeeById() {
        this.routeSubscription = this.activatedRoute.params
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe((parameters: Params) => {
          this.employeeId = parameters['id'];

            this.employeeService
              .getById(this.employeeId)
              .pipe(takeUntil(this.ngUnsubscribe))
              .subscribe(result => {
                this.employee = result;
              })
          
        });
    }
  public updateEmployee(): void {

      this.employeeService.update(this.employee).subscribe(result => {
        debugger
        this.snackBar.open('Success, employee updated', null, {
            duration: 1000
          });
        this.router.navigate(['../'], { relativeTo: this.activatedRoute });
      },
      error => {
        this.errors = JSON.parse(error)
        if(this.errors)
        {
          for (let i = 0; i < this.errors.errors.length; i++) {
            this.snackBar.open(this.errors.errors[i].message, null, {
              duration: 2000
            });
          }
        }
      },
      () => {
        // 'onCompleted' callback.
        // No errors, route to new page here
      })

  }
}
