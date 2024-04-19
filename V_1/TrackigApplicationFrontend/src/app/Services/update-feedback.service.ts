import { Observable } from "rxjs/internal/Observable";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map } from "rxjs";
import { Batch } from '../Models/batch';

@Injectable({
  providedIn: 'root'
})
export class UpdateFeedbackService {
private apiUrl='http://localhost:5138/TaskSubmission/UpdateFeedbacks';
constructor(private http: HttpClient) { }
Getall(Feedbacks:any): Observable<any> {

  const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  return this.http.post<any>(this.apiUrl,{Feedbacks}, httpOptions)
  .pipe(
    map(response => response.message));
}
}
