import { HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserForAuthenticationDto } from 'src/app/models/login.model';
import { AuthResponseDto } from 'src/app/models/authResponse.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private returnUrl= 'films';
  loginForm!: FormGroup;
  errorMessage: string = '';
  showError: boolean | undefined;
  user!: User;

  constructor(private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) { }
  
  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
    })
    this.returnUrl = '';
  }

  toRegistration=()=>{
    this.router.navigate(['/register']); 
  }

  toPass=()=>{
    this.router.navigate(['/forgotPassword']); 
  }

  validateControl = (controlName: string) => {
    return this.loginForm?.get(controlName)?.invalid && this.loginForm?.get(controlName)?.touched
  }

  hasError = (controlName: string, errorName: string) => {
    return this.loginForm?.get(controlName)?.hasError(errorName)
  }

  loginUser = (loginFormValue:any) => {
    this.showError = false;
    const login = {... loginFormValue };

    const userForAuth: UserForAuthenticationDto = {
      userName: login.username,
      password: login.password
    }

    this.authService.loginUser(userForAuth)
    .subscribe({
      next: (res:AuthResponseDto) => {
       localStorage.setItem("token", res.token);
       localStorage.setItem("userId", res.user.id.toString());
       this.router.navigate([this.returnUrl]);
       this.user = res.user;
    },
    error: (err: AuthResponseDto) => {
      this.errorMessage = 'Неверный логин или пароль';
      this.showError = true;
      setTimeout(()=>{
        this.errorMessage = '';
        this.showError = false;
      },3000)
    }})
  }
};
