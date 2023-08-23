import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Genre } from '../models/genre.model';

@Injectable({
  providedIn: 'root',
})
export class GenresService {
  apiURL = 'https://localhost:5001/api';
  constructor(private http: HttpClient) {}
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  getGenres(): Observable<Genre[]> {
    return this.http
      .get<Genre[]>(`${this.apiURL}/genres`)
  }
}