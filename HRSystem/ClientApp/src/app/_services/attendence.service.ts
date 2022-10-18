import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { CreateEmployee } from '@app/_models/CreateEmployee';
import { Observable } from 'rxjs';
import { Manager } from '@app/_models/manager';
import { EmployeeById } from '@app/_models/employeeById';
import { AttendancePagedList } from '@app/_models/attendanceList';
import { AttendanceLog } from '@app/_models/attendanceLog';


@Injectable({ providedIn: 'root' })
export class AttendanceService {
    constructor(private http: HttpClient) { }

    getAll(page:number,pageSize:number) {
        return this.http.get<AttendancePagedList>(`${environment.apiUrl}/GetLog?page=${page}&pageSize=${pageSize}`);
    }

    Log(attendanceLog: AttendanceLog): Observable<Object>{
        return this.http.post(`${environment.apiUrl}/Log`, attendanceLog, { responseType: 'text' });
    }
}