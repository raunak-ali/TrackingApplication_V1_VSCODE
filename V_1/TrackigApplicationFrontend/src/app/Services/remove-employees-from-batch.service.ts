import { Observable } from "rxjs/internal/Observable";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError, map, throwError } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class RemoveEmployeesFromBatchService {

  private apiurl = 'http://localhost:5138/Batch/RemoveUserFromABatch';
  constructor(private http: HttpClient) { }


  httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
  RemoveEmployee(Userid:number,Batchid:number): Observable<any> {
    const data = {
      Userid: Userid,
      BatchId:Batchid,

    };
  return this.http.post<any>(this.apiurl,data, this.httpOptions)
  .pipe(
    map(response => response.message));
}
}
