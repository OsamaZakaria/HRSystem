import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { Department } from '@app/_models/department';

@Injectable({ providedIn: 'root' })
export class DepartmentService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Department[]>(`${environment.apiUrl}/GetDepartments`);
    }

}