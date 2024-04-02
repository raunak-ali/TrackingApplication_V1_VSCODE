import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

private tokenKey = 'auth_token';
  private User!:any;

  private apiUrl = 'http://localhost:5138/User/Login';

  constructor(private http: HttpClient) { }

  login(formData: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl, formData, httpOptions).pipe(
      tap(response => {
        if (response && response.token) {
          this.saveToken(response.token);
        }
        if(response && response.userProfile){
          this.saveUser(response.userProfile);
          this.savecurrentUser(response.userProfile);
        }
      })
    );
  }
  private saveUser(User:any): void {
    sessionStorage.setItem(this.User,User.UserId);
  }

  getUser(): string |null {
    return sessionStorage.getItem(this.User);
  }

  clearUser(): void {
    sessionStorage.removeItem(this.User);
  }


  private saveToken(token: string): void {
    sessionStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return sessionStorage.getItem(this.tokenKey);
  }

  clearToken(): void {
    sessionStorage.removeItem(this.tokenKey);
  }


  private savecurrentUser(user: any): void {
    sessionStorage.setItem('current_User', JSON.stringify(user));
  }

  getcurrentUser(): any | null {
    const userJson = sessionStorage.getItem('current_User');
    return userJson ? JSON.parse(userJson) : null;
  }

  clearcurrentUser(): void {
    sessionStorage.removeItem('current_User');
  }
}
