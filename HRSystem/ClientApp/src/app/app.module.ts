import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// used to create fake backend
import { fakeBackendProvider } from './_helpers';

import { AppComponent } from './app.component';
import { appRoutingModule } from './app.routing';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { EmployeeListComponent } from './employee/employeeList.Component';
import { MatFormFieldModule, MatInputModule, MatTableModule, MatPaginatorModule,
     MatButtonModule,MatIconModule, MatSnackBarModule, 
     MatDatepickerModule, MatNativeDateModule, MatOptionModule ,MatSelectModule } from '@angular/material';
import { AddEmployeeComponent } from './employee/add-employee.Component';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        appRoutingModule,
        MatFormFieldModule,
        MatInputModule,
        MatTableModule,
        MatPaginatorModule,
        BrowserAnimationsModule,
        MatButtonModule,
        MatIconModule,
        MatSnackBarModule,
        FormsModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatOptionModule,
        MatSelectModule 
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        EmployeeListComponent,AddEmployeeComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

        // provider used to create fake backend
        fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }