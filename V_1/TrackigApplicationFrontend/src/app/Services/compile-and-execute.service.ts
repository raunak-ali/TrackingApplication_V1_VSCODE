import { Observable } from "rxjs/internal/Observable";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map } from "rxjs";
import { Batch } from '../Models/batch';
import { User } from "../Models/user";

@Injectable({
  providedIn: 'root'
})
export class CompileAndExecuteService {
CompileCode(code:string,sampleInput:string): Observable<any> {
    const data = {
      Code:code,
      SampleInput:sampleInput,

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

