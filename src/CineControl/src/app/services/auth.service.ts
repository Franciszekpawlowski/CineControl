import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, tap, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { GetUserResponseModel } from '../models/Response/get-user-response.model';
import { environment } from '../../environments/environment.prod'; 

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authStatus = new BehaviorSubject<boolean>(this.isAuthenticated());
  private authApiUrl = environment.AuthApiUrl; 
  private userApiUrl = environment.UserApiUrl; 
  private tenantId = environment.tenantId; 

  constructor(private http: HttpClient, private router: Router) {}

  /**
   * Tworzy nagłówki z `X-TenantId`
   */
  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'X-TenantId': this.tenantId,
    });
  }

  /**
   * Logowanie użytkownika
   */
  login(credentials: { email: string; password: string }): Observable<any> {
    const payload = {
      username: credentials.email,
      password: credentials.password,
    };

    return this.http.post<any>(`${this.authApiUrl}/Login`, payload, { headers: this.getHeaders() }).pipe(
      tap((response) => {
        if (response.accessToken) {
          sessionStorage.setItem('authToken', response.accessToken);
          this.authStatus.next(true);
        }
      })
    );
  }

  /**
   * Rejestracja nowego użytkownika
   */
  register(data: { name: string; email: string; password: string }): Observable<any> {
    const payload = {
      username: data.name,
      email: data.email,
      password: data.password,
    };

    return this.http.post<any>(`${this.authApiUrl}/Register`, payload, { headers: this.getHeaders() }).pipe(
      tap((response) => {
        // Obsługa rejestracji
      })
    );
  }

  /**
   * Wylogowanie użytkownika
   */
  logout(): void {
    sessionStorage.removeItem('authToken');
    this.authStatus.next(false);
    this.router.navigate(['/']);
  }

  /**
   * Sprawdza, czy użytkownik jest zalogowany
   */
  isAuthenticated(): boolean {
    return !!sessionStorage.getItem('authToken');
  }

  /**
   * Pobiera token uwierzytelniania
   */
  getToken(): string | null {
    return sessionStorage.getItem('authToken');
  }

  /**
   * Pobiera status uwierzytelnienia
   */
  getAuthStatus(): Observable<boolean> {
    return this.authStatus.asObservable();
  }

  /**
   * Pobiera informacje o zalogowanym użytkowniku
   */
  getUser(): Observable<GetUserResponseModel> {
    return this.http.get<GetUserResponseModel>(`${this.userApiUrl}/GetUser`, {
      headers: this.getHeaders(),
    });
  }
}
