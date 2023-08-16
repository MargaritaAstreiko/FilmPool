import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-filmHeaders',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent  {
    constructor(
        private _authService: AuthenticationService,
        private router: Router, 

    ) {}
    
    logout=()=>{
        this._authService.logoutUser();
        this.router.navigate(['/login']); 
    }
  

}

