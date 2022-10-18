import {Component, Injector, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatSort, MatTableDataSource, PageEvent, Sort} from '@angular/material';
import { SimpleDataSource } from '@app/_helpers/SimpleDataSource';
import { AttendanceService } from '@app/_services/attendence.service';
import { EmployeeService } from '@app/_services/employee.service';


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

  displayedColumns = ['employeeName', 'logDate','totalWorkingHours'];

         
  public attendanceDataSource: SimpleDataSource | null;


  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort: MatSort;

  constructor(injector: Injector, private attendanceService: AttendanceService) {

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


  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
  }
}

