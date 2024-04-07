import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddNewRatingService {
  private apiUrl = 'http://localhost:5138/TaskSubmission/RateASubmittedTask';

  constructor(private http: HttpClient) { }

  AddRating(addRating: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{addRating}, httpOptions).pipe(
      map(response => response.message))
    ;
}

}
