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
export class TaskSubmissionsService {

  private apiUrl = 'http://localhost:5138/TaskSubmission/ GetSubmOfaSubtaskbyUser';
  private MentorUrl='http://localhost:5138/TaskSubmission/GetSubmOfaSubtask';
  constructor(private http: HttpClient) { }
  Getall(subtaskid:any,userid:any): Observable<any> {

    const data = { subtaskid: subtaskid, userid: userid }; // Create an object with sbtaskid and usedid
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.http.post<any>(this.apiUrl,data, httpOptions)
    .pipe(
      map(response => response.message));
  }
  GetAllForMentor(subtaskid: number) {
    // Create an object with sbtaskid and usedid
   const httpOptions = {
     headers: new HttpHeaders({
       'Content-Type': 'application/json'
     })
   };

   return this.http.post<any>(this.MentorUrl,{subtaskid}, httpOptions)
   .pipe(
     map(response => response.message));
 }

}
