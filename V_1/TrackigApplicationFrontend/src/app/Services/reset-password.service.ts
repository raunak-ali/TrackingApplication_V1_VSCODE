import { Observable } from "rxjs/internal/Observable";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError, map, throwError } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService {

  private sendemailurl = 'http://localhost:5138/User/ResetPasswordOtp';
  private resetpasswordurl="http://localhost:5138/User/ResetPassword";
  constructor(private http: HttpClient) { }


  httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
  sendemail(capgeminiid:string){
    return this.http.post<any>(this.sendemailurl,{capgeminiid}, this.httpOptions)
    .pipe(
      map(response => response.message));

  }
  resetpassword(username: string, oldpassword: string, newpassword: string): Observable<any> {
      const data = {
        username: username,
        oldpassword: oldpassword,
        newpassword: newpassword
      };
    return this.http.post<any>(this.resetpasswordurl,data, this.httpOptions)
    .pipe(
      map(response => response.message));
  }
}
