import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Collection } from '../models/collection.model';
import { FilmToCollection } from '../models/filmAddToCollection.model';
import { env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})


export class CollectionsService {
  apiURL = env.apiUrl;
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

  addToCollection( addFilm: FilmToCollection): Observable<boolean>{
    return this.http.post<boolean>(`${this.apiURL}/collections/addFilm`, addFilm)

  }

  removeCollection( id: number): Observable<boolean>{
    return this.http.delete<boolean>(`${this.apiURL}/collections/${id}`)

  }

}