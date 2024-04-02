import { Observable } from "rxjs/internal/Observable";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class GetBatchesService {
  private apiUrl = 'http://localhost:5138/Batch/GetAllBatches';
  constructor(private http: HttpClient) { }
  Getall(MentorId:any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{MentorId}, httpOptions)
    .pipe(
      map(response => response.message));
  }
}
