import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-filmHeaders',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit  {
    user!:User

    constructor(
        private _authService: AuthenticationService,
        private router: Router, 

    ) {}

    ngOnInit() {
        const id = localStorage.getItem("userId")||0;
        this._authService.getUserInfo(+id).subscribe(data=>{
        this.user=data;
        });
    
    }

    logout=()=>{
        this._authService.logoutUser();
        this.router.navigate(['/login']); 
    }
  

}

