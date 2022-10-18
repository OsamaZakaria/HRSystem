import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateEmployee } from '@app/_models/CreateEmployee';
import { Department } from '@app/_models/department';
import { ServerErrors } from '@app/_models/error';
import { Manager } from '@app/_models/manager';
import { DepartmentService } from '@app/_services/department.service';
import { EmployeeService } from '@app/_services/employee.service';



@Component({
  templateUrl: 'add-employee.component.html'
})
export class AddEmployeeComponent implements OnInit{
  public employee = new CreateEmployee();
  public managers:Manager[];
  public departments:Department[];
  errors: ServerErrors;
  constructor(injector: Injector, private employeeService: EmployeeService, private departmentService: DepartmentService,
    private snackBar:MatSnackBar, private router:Router,
    private activatedRoute:ActivatedRoute) {
  }
    ngOnInit(): void {
        this.GetManagers();
        this.GetDepartments();
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
  public creatNewEmployee(): void {

      this.employeeService.create(this.employee).subscribe(result => {
        debugger
        console.log(result);
        this.snackBar.open('Success, New employee created', null, {
            duration: 1000
          });
        this.router.navigate(['../'], { relativeTo: this.activatedRoute });
      },
      error => {
        console.log(error)
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
      }
      )

  }
}
