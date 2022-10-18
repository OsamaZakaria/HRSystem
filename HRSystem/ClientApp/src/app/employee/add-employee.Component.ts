import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateEmployee } from '@app/_models/CreateEmployee';
import { Manager } from '@app/_models/manager';
import { EmployeeService } from '@app/_services/employee.service';



@Component({
  templateUrl: 'add-employee.component.html'
})
export class AddEmployeeComponent implements OnInit{
  public employee = new CreateEmployee();
  public managers:Manager[];
  constructor(injector: Injector, private employeeService: EmployeeService, 
    private snackBar:MatSnackBar, private router:Router,
    private activatedRoute:ActivatedRoute) {
  }
    ngOnInit(): void {
        this.GetManagers();
    }
    private GetManagers() {
        this.employeeService.getManagers('').subscribe((result) => {
            this.managers = result;
            console.log(result);
        });
    }
  public creatNewEmployee(): void {

      this.employeeService.create(this.employee).subscribe(result => {

        this.snackBar.open('Success, New employee created', null, {
            duration: 1000
          });
        this.router.navigate(['../'], { relativeTo: this.activatedRoute });
      })

  }
}
