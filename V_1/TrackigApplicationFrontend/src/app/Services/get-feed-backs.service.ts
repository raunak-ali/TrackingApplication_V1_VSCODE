import { Observable } from "rxjs/internal/Observable";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map } from "rxjs";
import { Batch } from '../Models/batch';

import { catchError, throwError } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class GetFeedBacksService {
  constructor(private http: HttpClient) { }
  GetAll(Taskid: number) {
    const taskid = Number(Taskid);

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    var res=this.http.post<any>(this.apiUrl,{taskid},httpOptions);
    return res.pipe(
      map(response => this.resolveReferences(response))
    );
  }

  private resolveReferences(data: any): any[] {
    // Create a dictionary to store objects by their ID
    const objectsById: { [key: string]: any } = {};

    // Iterate through the response and store objects with IDs in the dictionary
    const traverse = (obj: any) => {
      if (obj && obj.$id) {
        objectsById[obj.$id] = obj;
        delete obj.$id;
      }
      for (const prop in obj) {
        if (typeof obj[prop] === 'object') {
          traverse(obj[prop]);
        }
      }
    };

    traverse(data);

    // Replace references with actual objects
    const resolveRef = (obj: any): any => {
      if (obj && obj.$ref) {
        return objectsById[obj.$ref];
      }
      for (const prop in obj) {
        if (typeof obj[prop] === 'object') {
          obj[prop] = resolveRef(obj[prop]);
        }
      }
      return obj;
    };

    return resolveRef(data) as any[];
  }

  private apiUrl = 'http://localhost:5138/Task/GetTaskFeedBack'; // Replace 'your-api-url' with your actual API endpoint




}
