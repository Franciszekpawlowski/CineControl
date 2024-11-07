import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  login(credentials: { email: string; password: string }): Observable<any> {
    const payload = {
      username: credentials.email,
      password: credentials.password,
    };
    return this.http.post<any>('/Auth/Login', payload).pipe(
      tap((response) => {
        if (response.accessToken) {
          sessionStorage.setItem('authToken', response.accessToken);
        }
      })
    );
  }

  register(data: { name: string; email: string; password: string }): Observable<any> {
    const payload = {
      username: data.name,
      email: data.email,
      password: data.password,
    };
    return this.http.post<any>('/Auth/Register', payload).pipe(
      tap((response) => {
      })
    );
  }
  

  logout(): void {
    sessionStorage.removeItem('authToken');
  }

  isAuthenticated(): boolean {
    return !!sessionStorage.getItem('authToken');
  }

  getToken(): string | null {
    //console.log(sessionStorage.getItem('authToken'));
    return sessionStorage.getItem('authToken');
  }
}
