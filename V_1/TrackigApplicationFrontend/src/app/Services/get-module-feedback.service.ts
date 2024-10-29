import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GetModuleFeedbackService {
//http://localhost:5138/TaskSubmission/GetFeedbackforAModule
private apiUrl = 'http://localhost:5138/TaskSubmission/GetFeedbackforAModule';
constructor(private http: HttpClient) { }
GetModuleFeedback(ModuleId:number): Observable<any> {

  const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  return this.http.post<any>(this.apiUrl,{ModuleId},httpOptions)
  .pipe(
    map(response => response.message));
}
}
