import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProcteredService {
  private apiUrl = 'http://localhost:5138/Proctered/AddProctered';
  constructor(private http: HttpClient) { }
  AddProct(usermodule:any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{usermodule}, httpOptions)
    .pipe(
      map(response => response.message));
  }
  private GetUrl='http://localhost:5138/Proctered/GetALlProcts';
  GetProct(): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.GetUrl, httpOptions)
    .pipe(
      map(response => response.message));
  }
  private ApproveUrl='http://localhost:5138/Proctered/ApprovProct';
  ApproveProct(proctid:any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.ApproveUrl,{proctid}, httpOptions)
    .pipe(
      map(response => response.message));
  }
}
