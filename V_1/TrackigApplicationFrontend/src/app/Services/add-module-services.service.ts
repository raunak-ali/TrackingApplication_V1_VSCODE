import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddModuleServicesService {

  private apiUrl = 'http://localhost:5138/Module/AddModule';

  constructor(private http: HttpClient) { }

  AddnewModule(usermodule: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{usermodule}, httpOptions).pipe(
      map(response => response.message))
    ;
}
}
