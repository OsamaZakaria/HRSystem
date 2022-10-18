import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { EmployeePagedList } from '@app/_models/employee';
import { CreateEmployee } from '@app/_models/CreateEmployee';
import { Observable } from 'rxjs';
import { Manager } from '@app/_models/manager';
import { EmployeeById } from '@app/_models/employeeById';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
    constructor(private http: HttpClient) { }

    getAll(page:number,pageSize:number, search:string) {
        return this.http.get<EmployeePagedList>(`${environment.apiUrl}/GetEmployees?page=${page}&pageSize=${pageSize}&search=${search}`);
    }
    getById(id:string) {
        return this.http.get<EmployeeById>(`${environment.apiUrl}/GetEmployeeById?id=${id}`);
    }
    getManagers(id:string) {
        return this.http.get<Manager[]>(`${environment.apiUrl}/GetManagers?currentEmployeeId=${id}`);
    }

    create(employee: CreateEmployee): Observable<Object>{
        return this.http.post(`${environment.apiUrl}/CreateEmployee`, employee, { responseType: 'text' });
    }
    update(employee: EmployeeById): Observable<Object>{
        return this.http.post(`${environment.apiUrl}/UpdateEmployee`, employee, { responseType: 'text' });
    }
}