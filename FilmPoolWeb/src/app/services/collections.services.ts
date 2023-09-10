import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Collection } from '../models/collection.model';

@Injectable({
  providedIn: 'root',
})
export class CollectionsService {
  apiURL = 'https://localhost:5001/api';
  constructor(private http: HttpClient) {}
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  getCollection(userId:number): Observable<Collection[]> {
    return this.http
      .get<Collection[]>(`${this.apiURL}/collections/user${userId}`)
  }

  createCollection(collection:Collection): Observable<boolean> {
    return this.http
      .post<boolean>(`${this.apiURL}/collections`,collection)
  }

}