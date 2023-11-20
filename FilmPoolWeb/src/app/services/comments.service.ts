import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Comment } from '../models/comment.model';
import { CommentCreateModel } from '../models/commentcreate.model';

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

  leaveComments(comment:CommentCreateModel): Observable<boolean> {
    return this.http
      .post<boolean>(`${this.apiURL}/comments`,comment)
  }

  updateComments(id:number, userId: number, comment: string, createdDate: Date  ): Observable<boolean> {
    return this.http
      .post<boolean>(`${this.apiURL}/comments/${id}`,{id: id,userId: userId, comment: comment, createdDate: createdDate })
  }

  removeComment( id: number): Observable<boolean>{
    return this.http.delete<boolean>(`${this.apiURL}/comments/${id}`)

  }

}