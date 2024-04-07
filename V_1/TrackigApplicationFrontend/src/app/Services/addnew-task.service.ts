import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddnewTaskService {

  private apiUrl = 'http://localhost:5138/Task/AddTask';

  constructor(private http: HttpClient) { }

  AddTask(task: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{task}, httpOptions).pipe(
      map(response => response.message))
    ;
}
}
