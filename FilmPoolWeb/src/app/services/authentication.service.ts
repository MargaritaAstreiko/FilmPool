import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { UserForAuthenticationDto } from '../models/login.model';
import { AuthResponseDto } from '../models/authResponse.model';
import { UserForRegistratioModel } from '../models/userRegistration.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ForgotPasswordModel } from '../models/forgotPassword.model';
import { ResetPasswordModel } from '../models/resetPassword.model';
import { User } from '../models/user.model';
import { env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})

export class AuthenticationService {
  apiURL = env.apiUrl;
  user!:User;
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  }

  loginUser(userForAuth: UserForAuthenticationDto): Observable<AuthResponseDto> {
    return this.http
      .post<AuthResponseDto>(`${this.apiURL}/login`, userForAuth)
  }

  logoutUser = () => {
    localStorage.removeItem("token");
  }

  setUser(){
    const userId = localStorage.getItem("userId")||0;
    this.getUserInfo(+userId).subscribe(data => {
      this.user = data;
    })
  }

  getUser(){
    return this.user;
  }

  getUserInfo(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiURL}/users/${id}`);
  }

  registerUser = (body: UserForRegistratioModel) => {
    return this.http.post<UserForRegistratioModel>(`${this.apiURL}/registration`, body);
  }

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");

    return token ? token?.length > 0 && !this.jwtHelper.isTokenExpired(token) : false;
  }

  isUserAdmin = (): boolean => {
    const token = localStorage.getItem("token");
    const decodedToken = token ? this.jwtHelper.decodeToken(token) : '';
    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    return role === 'Admin';
  }

  forgotPassword(body: ForgotPasswordModel): Observable<any> {
    return this.http.post<any>(`${this.apiURL}/forgot-password`, body);

  }

  public resetPassword(body: ResetPasswordModel): Observable<any> {
    return this.http.post<any>(`${this.apiURL}/reset-password`, body);
  }
}