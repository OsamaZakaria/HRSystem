import {Component, Injector, OnInit, ViewChild} from '@angular/core';
import {MatDialog, MatPaginator, MatSort, MatTableDataSource, PageEvent, Sort} from '@angular/material';
import { SimpleDataSource } from '@app/_helpers/SimpleDataSource';
import { AttendanceList } from '@app/_models/attendanceList';
import { AttendanceService } from '@app/_services/attendance.service';
import { EmployeeService } from '@app/_services/employee.service';
import { EmployeeAttendenceDetailsDaialog } from './employee-attendence-details-daialog';


/**
 * @title Data table with sorting, pagination, and filtering.
 */
@Component({
  selector: 'attendance-list',
  styleUrls: ['attendance.css'],
  templateUrl: 'attendance.Component.html',
})
export class AttendanceComponent implements OnInit {

    public pageIndex = 0;
    public searchTerm = " ";
    public totalCount = 0;
    public pageSize = 5;
    public pageSizeOptions = [5, 10, 25, 100];

  displayedColumns = ['employeeName', 'logDate','totalWorkingHours', 'logDetails'];

         
  public attendanceDataSource: SimpleDataSource | null;


  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort: MatSort;

  constructor(injector: Injector, private attendanceService: AttendanceService,private dialog: MatDialog) {

  }
  ngOnInit(): void {
    this.refreshAttendance();
   }
   private refreshAttendance() {

    this.attendanceService.getAll(this.pageIndex, this.pageSize).subscribe((result) => {
        this.totalCount = result.totalCount;
        this.attendanceDataSource = new SimpleDataSource(result.items);
        console.log(result);
        console.log(this.attendanceDataSource);
    });
}
pageChangehandler(pagingData: PageEvent) {
    this.pageIndex = pagingData.pageIndex;
    this.pageSize = pagingData.pageSize;
    this.refreshAttendance();
}

openDialog(_data:AttendanceList): void {
  const dialogRef = this.dialog.open(EmployeeAttendenceDetailsDaialog, {
    width: '450px',
    data: _data
  });

  dialogRef.afterClosed().subscribe(result => {
    console.log('The dialog was closed');
  });
}
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
  }
}

