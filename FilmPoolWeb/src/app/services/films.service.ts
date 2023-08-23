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


  getFilms(pageSize: number, currentPage: number, search: string, genre?: Genre): Observable<FilmResponseModel> {
    return this.http
      .post<FilmResponseModel>(`${this.apiURL}/films`,{pageSize:pageSize,currentPage:currentPage, search:search, genre:genre})
  }

  getFilm(id: number): Observable<FilmInfo>{
    return this.http
    .get<FilmInfo>(`${this.apiURL}/films/${id}`,)

  }

  updateFilm(film: Film): Observable<boolean>{
    return this.http
    .post<boolean>(`${this.apiURL}/films/${film.id}`,{...film})

  }

  picture(id: number, file: FormData):Observable<boolean>{
    console.log(file)
    return this.http
    .post<boolean>(`${this.apiURL}/films/Picture`,file).pipe(
      tap(data => data)
    );
  }

}