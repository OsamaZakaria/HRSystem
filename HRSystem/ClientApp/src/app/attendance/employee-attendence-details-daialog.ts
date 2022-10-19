import {Component, Inject} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { AttendanceList } from '@app/_models/attendanceList';

@Component({
    selector: 'employee-attendence-details-daialog',
    templateUrl: 'employee-attendence-details-daialog.html',
  })
  export class EmployeeAttendenceDetailsDaialog {
  

    constructor(
      public dialogRef: MatDialogRef<EmployeeAttendenceDetailsDaialog>,
      @Inject(MAT_DIALOG_DATA) public data: AttendanceList) {
        console.log('EmployeeAttendenceDetailsDaialog')
        console.log(data)
      }
  
    onNoClick(): void {
      this.dialogRef.close();
    }
  
  }