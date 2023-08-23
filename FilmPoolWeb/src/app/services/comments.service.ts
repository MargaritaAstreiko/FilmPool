import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Comment } from '../models/comment.model';

@Injectable({
  providedIn: 'root',
})
export class CommentsService {
  apiURL = 'https://localhost:5001/api';
  constructor(private http: HttpClient) {}
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  getComments(filmId:number): Observable<Comment[]> {
    return this.http
      .post<Comment[]>(`${this.apiURL}/comments/film`,{currentPage:1,pageSize:0,filmId: filmId})
  }

  leaveComments(comment:Comment): Observable<boolean> {
    return this.http
      .post<boolean>(`${this.apiURL}/comments`,comment)
  }

}