import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authStatus = new BehaviorSubject<boolean>(this.isAuthenticated());

  constructor(private http: HttpClient, private router: Router) {}

  login(credentials: { email: string; password: string }): Observable<any> {
    const payload = {
      username: credentials.email,
      password: credentials.password,
    };
    return this.http.post<any>('/Account/Login', payload).pipe(
      tap((response) => {
        if (response.accessToken) {
          sessionStorage.setItem('authToken', response.accessToken);
          this.authStatus.next(true);
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
    return this.http.post<any>('/Account/Register', payload).pipe(
      tap((response) => {
      })
    );
  }

  logout(): void {
    sessionStorage.removeItem('authToken');
    this.authStatus.next(false);
    this.router.navigate(['/'])
  }

  isAuthenticated(): boolean {
    return !!sessionStorage.getItem('authToken');
  }

  getToken(): string | null {
    return sessionStorage.getItem('authToken');
  }

  getAuthStatus(): Observable<boolean> {
    return this.authStatus.asObservable();
  }
}
