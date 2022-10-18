import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';


import { AuthenticationService } from './_services';
import { User } from './_models';
import { AttendanceService } from './_services/attendence.service';
import { AttendanceLog } from './_models/attendanceLog';


@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent {
    currentUser: User;
    employee:AttendanceLog;
    public logAction:string;

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
        private attendance: AttendanceService
    ) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
        this.getLastAction();
    }
    getLastAction()
    {
        debugger
        this.attendance.getLastAction(this.currentUser.employeeId).subscribe((result) => {
            console.log(result)
            this.logAction = result;
        },
        error => {
          debugger
          console.log(error);
        },
        () => {
          // 'onCompleted' callback.
          // No errors, route to new page here
        });
    }
    logAttendanceAction()
    {
        if(confirm("Are you sure to " + this.logAction)) {
        if(this.currentUser.isEmployee)
        {
           debugger;
           this.employee = new AttendanceLog();
            this.employee.employeeId =this.currentUser.employeeId;
            this.attendance.Log(this.employee).subscribe(result => {
            this.getLastAction();
            alert("Success")
          })
        }
    }
    }
    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
        
    }
}