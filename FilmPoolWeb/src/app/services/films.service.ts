import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { Film } from '../models/film.model';
import { FilmResponseModel } from '../models/filmsResponseModel';
import { Genre } from '../enums/genre.enum';
import { FilmInfo } from '../models/film-info.model';
import { FilmLightModel } from '../models/filmLight.model';
import { env } from 'src/environments/environment';

@Injectable()

export class FilmsService {
  apiURL = env.apiUrl;
  token = localStorage.getItem("token");
  constructor(private http: HttpClient) {}
   headers =  new HttpHeaders().set(
      'Authorization', `Bearer ${this.token}`
    )


  getFilms(pageSize: number, currentPage: number, year?: number, search?: string, genre?: Genre, rating?: boolean, collectionId?: number ) : Observable<FilmResponseModel> {
    return this.http
      .post<FilmResponseModel>(`${this.apiURL}/films`,{pageSize:pageSize,currentPage:currentPage, year:year, search:search, genre:genre, rating:rating, collectionId:collectionId})
  }

  getFilm(id: number): Observable<FilmInfo>{
    return this.http
    .get<FilmInfo>(`${this.apiURL}/films/${id}`,)

  }

  updateFilm(film: Film): Observable<boolean>{
    return this.http
    .post<boolean>(`${this.apiURL}/films/${film.id}`,{...film})

  }

  
  createFilm(film: Film): Observable<number>{
    return this.http
    .post<number>(`${this.apiURL}/films/new`,{...film})

  }

  picture(id: number, file: FormData):Observable<boolean>{
    return this.http
    .post<boolean>(`${this.apiURL}/films/Picture/${id}`,file).pipe(
      tap(data => data)
    );
  }

  getFilmsLight(): Observable<FilmLightModel[]>{
    return this.http
    .get<FilmLightModel[]>(`${this.apiURL}/films/Light`,)

  }

  deleteFilm( id: number): Observable<number>{
    return this.http.delete<number>(`${this.apiURL}/films/${id}`)

  }


}