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
        this.logAction = 'Check-In'
    }
    getLastAction()
    {
        this.attendance.getLastAction(this.currentUser.employeeId).subscribe((result) => {
            this.logAction =result;
        });
    }
    logAttendanceAction()
    {
        if(this.currentUser.isEmployee)
        {
          this.employee.employeeId =this.currentUser.employeeId;
           this.attendance.Log(this.employee)
           this.attendance.Log(this.employee).subscribe(result => {
            this.getLastAction();
          })
        }
    }
    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
        
    }
}