import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GetModuleServicesService {

  private apiUrl = 'http://localhost:5138/Module/GetAllModules';
  constructor(private http: HttpClient) { }
  GetModules(batchid:number): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{batchid},httpOptions)
    .pipe(
      map(response => response.message));
  }
}
