import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Movie } from '../models/movie.model';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  private apiUrl = '/Movies';
  private tenantId = '3fa85f64-5717-4562-b3fc-2c963f66afa6'; 
  constructor(private http: HttpClient) {}

  /**
   * Tworzy nagłówki z X-TenantId
   */
  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'X-TenantId': this.tenantId,
    });
  }

  /**
   * Pobiera listę aktualnie wyświetlanych filmów.
   */
  getCurrentMovies(): Observable<Movie[]> {
    return this.http
      .get<Movie[]>(`${this.apiUrl}/current`, { headers: this.getHeaders() })
      .pipe(catchError((error: HttpErrorResponse) => this.handleHttpError(error)));
  }

  /**
   * Pobiera listę nadchodzących filmów.
   */
  getUpcomingMovies(): Observable<Movie[]> {
    return this.http
      .get<Movie[]>(`${this.apiUrl}/upcoming`, { headers: this.getHeaders() })
      .pipe(catchError((error: HttpErrorResponse) => this.handleHttpError(error)));
  }

  /**
   * Pobiera listę najwyżej ocenianych filmów.
   */
  getTopRatedMovies(): Observable<Movie[]> {
    return this.http
      .get<Movie[]>(`${this.apiUrl}/top-rated`, { headers: this.getHeaders() })
      .pipe(catchError((error: HttpErrorResponse) => this.handleHttpError(error)));
  }

  /**
   * Pobiera szczegóły filmu.
   */
  getMovieDetails(id: number): Observable<Movie> {
    return this.http
      .get<Movie>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() })
      .pipe(catchError((error: HttpErrorResponse) => this.handleHttpError(error)));
  }

  /**
   * Pobiera spersonalizowane rekomendacje filmowe dla użytkownika.
   */
  getPersonalizedRecommendations(userId: number): Observable<Movie[]> {
    return this.http
      .get<Movie[]>(`${this.apiUrl}/recommendations/${userId}`, { headers: this.getHeaders() })
      .pipe(catchError((error: HttpErrorResponse) => this.handleHttpError(error)));
  }

  /**
   * Pobiera film na podstawie jego ID.
   */
  getMovieById(id: number): Observable<Movie> {
    return this.http
      .get<Movie>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() })
      .pipe(catchError((error: HttpErrorResponse) => this.handleHttpError(error)));
  }

  /**
   * Obsługuje błędy HTTP.
   */
  private handleHttpError(error: HttpErrorResponse): Observable<never> {
    const errorMessage =
      error.error instanceof ErrorEvent
        ? `Błąd klienta: ${error.error.message}`
        : `Błąd serwera ${error.status}: ${error.error.detail || error.message}`;
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
