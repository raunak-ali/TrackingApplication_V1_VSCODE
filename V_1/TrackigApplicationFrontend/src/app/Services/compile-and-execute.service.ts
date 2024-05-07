import { Observable } from "rxjs/internal/Observable";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map } from "rxjs";
import { Batch } from '../Models/batch';
import { User } from "../Models/user";
import { SubTask } from '../Models/sub-task';

@Injectable({
  providedIn: 'root'
})
export class CompileAndExecuteService {
  submiturl="http://localhost:5138/TryCompiler/SubmitCompiledCode";
  GetSubtaskUrl="http://localhost:5138/Task/GetSubtask";
GetSubTask(SubTaskId:number): Observable<any> {
  const data = {

    SubTaskId:SubTaskId

  };
  const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  return this.http.post<any>(this.GetSubtaskUrl,data, httpOptions)
  .pipe(
    map(response => response.message));
}





  SubmitCode(code: string, sampleInput: string, subtaskid: number, UserId: any): Observable<any> {
    const data = {
      Code:code,
      SampleInput:sampleInput,
      SubTaskId:subtaskid,
      userid:UserId

    };
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.submiturl,data, httpOptions)
    .pipe(
      map(response => response.message));
  }
CompileCode(code:string,sampleInput:string,subtaskid:number): Observable<any> {
    const data = {
      Code:code,
      SampleInput:sampleInput,
      SubTaskId:subtaskid

    };
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,data, httpOptions)
    .pipe(
      map(response => response.message));
  }
  private apiUrl='http://localhost:5138/TryCompiler';
constructor(private http: HttpClient) { }
}

