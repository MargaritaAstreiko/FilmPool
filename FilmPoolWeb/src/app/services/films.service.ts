import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { Film } from '../models/film.model';
import { FilmResponseModel } from '../models/filmsResponseModel';
import { Genre } from '../enums/genre.enum';
import { FilmInfo } from '../models/film-info.model';
@Injectable({
  providedIn: 'root',
})
export class FilmsService {
  apiURL = 'https://localhost:5001/api';
  token = localStorage.getItem("token");
  constructor(private http: HttpClient) {}
   headers =  new HttpHeaders().set(
      'Authorization', `Bearer ${this.token}`
    )

  // HttpClient API get() method => Fetch employees list
  getFilms(pageSize: number, currentPage: number, search: string, genre?: Genre): Observable<FilmResponseModel> {
    return this.http
      .post<FilmResponseModel>(`${this.apiURL}/films`,{pageSize:pageSize,currentPage:currentPage, search:search, genre:genre})
  }

  getFilm(id: number): Observable<FilmInfo>{
    return this.http
    .get<FilmInfo>(`${this.apiURL}/films/${id}`,)

  }

  picture(id: number, file: FormData):Observable<boolean>{
    console.log(file)
    return this.http
    .post<boolean>(`${this.apiURL}/films/Picture`,file).pipe(
      tap(data => data)
    );
  }
  /* HttpClient API get() method => Fetch employee
  getEmployee(id: any): Observable<Employee> {
    return this.http
      .get<Employee>(this.apiURL + '/employees/' + id)
      .pipe(retry(1), catchError(this.handleError));
  }
  // HttpClient API post() method => Create employee
  createEmployee(employee: any): Observable<Employee> {
    return this.http
      .post<Employee>(
        this.apiURL + '/employees',
        JSON.stringify(employee),
        this.httpOptions
      )
      .pipe(retry(1), catchError(this.handleError));
  }
  // HttpClient API put() method => Update employee
  updateEmployee(id: any, employee: any): Observable<Employee> {
    return this.http
      .put<Employee>(
        this.apiURL + '/employees/' + id,
        JSON.stringify(employee),
        this.httpOptions
      )
      .pipe(retry(1), catchError(this.handleError));
  }
  // HttpClient API delete() method => Delete employee
  deleteEmployee(id: any) {
    return this.http
      .delete<Employee>(this.apiURL + '/employees/' + id, this.httpOptions)
      .pipe(retry(1), catchError(this.handleError));
  }
  // Error handling
  handleError(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }*/
}