import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AddTaskSubmissionsService {

  private apiUrl = 'http://localhost:5138/TaskSubmission/AddSubmission';
//ddSubmission
  constructor(private http: HttpClient) { }

  AddSubmission(taskSubmissions: any): Observable<any> {
    //var temp=
    const base64String = taskSubmissions.FileUploadSubmission as string; // Explicitly cast to string
    const match = base64String.match(/data:.*;base64,(.*)/);
if (match && match.length === 2) {
    const cleanedBase64String = match[1]; // Extract the base64 string
    taskSubmissions.FileUploadSubmission = cleanedBase64String;
}

    //reader.readAsDataURL(file);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{taskSubmissions}, httpOptions)
    ;
  }
}
