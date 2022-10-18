import { Component, ViewChild } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService, AuthenticationService } from '@app/_services';
import { MatPaginator } from '@angular/material';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    loading = false;
    users: User[];

    constructor(private userService: UserService,private authenticationService: AuthenticationService) { }

    ngOnInit() {
        this.loading = true;
       
    }
}