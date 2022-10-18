import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';


import { AuthenticationService } from './_services';
import { User } from './_models';
import { AttendanceService } from './_services/attendance.service';
import { AttendanceLog } from './_models/attendanceLog';
import { GetLastAttendanceActionResponse } from './_models/getLastAttendanceActionResponse';


@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent implements OnInit {
    currentUser: User;
    employee:AttendanceLog;
    logAction:GetLastAttendanceActionResponse;

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
        private attendance: AttendanceService
    ) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);

    }
    ngOnInit(): void {
        this.getLastAction();
    }
    getLastAction()
    {
        this.attendance.getLastAction(this.currentUser.employeeId).subscribe((result) => {
            console.log(result)
            this.logAction = result;
        },
        error => {
          debugger
          console.log(error);
        },
        () => {
        });
    }
    logAttendanceAction()
    {
        if(confirm("Are you sure to " + this.logAction.action)) {
        if(this.currentUser.isEmployee)
        {
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