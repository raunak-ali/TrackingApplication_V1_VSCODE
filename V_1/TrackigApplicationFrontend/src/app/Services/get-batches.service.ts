import { Observable } from "rxjs/internal/Observable";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError, map, throwError } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class GetBatchesService {
  private apiUrl = 'http://localhost:5138/Batch/GetAllBatches';
  private anotherApiUrl="http://localhost:5138/Batch/GetAllBatchesForEmployees";
  private geturl='http://localhost:5138/Batch/GetAllBatch';
  getBatch(): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.geturl, httpOptions)
    .pipe(
      map(response => response.message));}
  constructor(private http: HttpClient) { }
  Getall(UserId:any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{UserId}, httpOptions)
    .pipe(
      map(response => response.message),
      catchError(error => {
        if (error) {
          // Trigger another HTTP POST request
          return this.anotherHttpPostRequest(UserId).pipe(
            map(response => response)
          );
        } else {
          // Re-throw the error if it's not a 401
          return throwError(error);
        }
      })
    );
  }
  anotherHttpPostRequest(UserId:any) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    // Implement your logic for the second HTTP POST request here
    return this.http.post<any>(this.anotherApiUrl, {UserId}, httpOptions) .pipe(
      map(response => response.message));
  }
}
