import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AddMentorService {
  //private User='active_user';

  private apiUrl = 'http://localhost:5138/User/AddMentor';

  constructor(private http: HttpClient) { }

  Addmentor(user: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,user, httpOptions).pipe(
      map(response => response.message))
    ;
}

}
