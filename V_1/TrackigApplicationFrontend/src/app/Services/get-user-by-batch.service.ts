import { Observable } from "rxjs/internal/Observable";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map } from "rxjs";

@Injectable({
  providedIn: 'root'
})
//http://localhost:5138/User/GetUserByBatch
export class GetUserByBatchService {

  private apiUrl = 'http://localhost:5138/User/GetUserByBatch';
  constructor(private http: HttpClient) { }
  Getall(BatchId:any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{BatchId}, httpOptions)
    .pipe(
      map(response => response.message));
  }
}
