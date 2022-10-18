import { Routes, RouterModule } from '@angular/router';
import { AttendanceComponent } from './attendance/attendance.component';
import { AddEmployeeComponent } from './employee/add-employee.Component';
import { EditEmployeeComponent } from './employee/edit-employee.component';
import { EmployeeListComponent } from './employee/employeeList.Component';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers';

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'employee', component: EmployeeListComponent, canActivate: [AuthGuard] },
    { path: 'employee/add', component: AddEmployeeComponent, canActivate: [AuthGuard] },
    { path: 'employee/edit/:id', component: EditEmployeeComponent, canActivate: [AuthGuard] },
    { path: 'attendence', component: AttendanceComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);