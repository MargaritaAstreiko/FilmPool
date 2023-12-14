import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Rating } from '../models/rating.model';
import { env } from 'src/environments/environment';

@Injectable()


export class RatingService {
  apiURL = env.apiUrl;
  constructor(private http: HttpClient) { }
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  putRating(rating: Rating): Observable<boolean> {
    return this.http
      .post<boolean>(`${this.apiURL}/rating`, rating)
  }

}