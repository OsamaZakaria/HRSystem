import {Component, Injector, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatSort, MatTableDataSource, PageEvent, Sort} from '@angular/material';
import { SimpleDataSource } from '@app/_helpers/SimpleDataSource';
import { EmployeeService } from '@app/_services/employee.service';


/**
 * @title Data table with sorting, pagination, and filtering.
 */
@Component({
  selector: 'employee-list',
  styleUrls: ['employee-list.css'],
  templateUrl: 'employeeList.Component.html',
})
export class EmployeeListComponent implements OnInit {

    public pageIndex = 0;
    public searchTerm = " ";
    public totalCount = 0;
    public pageSize = 5;
    public pageSizeOptions = [5, 10, 25, 100];

  displayedColumns = ['managerName', 'email','team'];
  nestedDisplayedColumns = ['employeeName', 'email'];

  public employeeDataSource: SimpleDataSource | null;


  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort: MatSort;

  constructor(injector: Injector, private employeeService: EmployeeService) {

  }
  ngOnInit(): void {
    this.refreshEmployees();
   }
   private refreshEmployees() {

    this.employeeService.getAll(this.pageIndex, this.pageSize,this.searchTerm).subscribe((result) => {
        this.totalCount = result.totalCount;
        this.employeeDataSource = new SimpleDataSource(result.items);
        console.log(result);
        console.log(this.employeeDataSource);
    });
}
pageChangehandler(pagingData: PageEvent) {
    this.pageIndex = pagingData.pageIndex;
    this.pageSize = pagingData.pageSize;
    this.refreshEmployees();
}

onSearchChange(searchValue: string): void {  
  this.searchTerm = searchValue;
  this.refreshEmployees()
}
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
  }
}

