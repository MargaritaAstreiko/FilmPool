import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../models/user.model';
import { Observable, throwError } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})
export class UsersService {
  apiURL = 'https://localhost:5001/api';
  constructor(private http: HttpClient) {}
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  getUsers(): Observable<User[]> {
    return this.http
      .get<User[]>(`${this.apiURL}/users`)
  }

  picture(id: number, file: FormData):Observable<boolean>{
    return this.http
    .post<boolean>(`${this.apiURL}/films/Picture/${id}`,file).pipe(
      tap(data => data)
    );
  }

  updateUser(user: User): Observable<boolean>{
    return this.http
    .post<boolean>(`${this.apiURL}/users/${user.id}`,{...user})

  }
}