import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

// Importowanie modeli
import { Seat } from '../models/seat.model';
import { Cinema } from '../models/cinema.model';
import { Theater } from '../models/theater.model';
import { GetCinemasByCityResponse } from '../models/Response/get-cinemas-by-city-response.model';
import { GetCitiesResponse } from '../models/Response/get-cities-response.model';
import { ErrorResponse } from '../models/Response/error-response.model';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class CinemaService {
  private cinemasApiUrl = environment.CinemasApiUrl; 
  private tenantId: string = environment.tenantId;

  constructor(private http: HttpClient) {}

  /**
   * Ustawienie nagłówka X-TenantId dla każdego żądania.
   */
  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'X-TenantId': this.tenantId,
    });
  }


  getCinemasByCity(city: string): Observable<Cinema[]> {
    const url = `${this.cinemasApiUrl}/bycity/${encodeURIComponent(city)}`;
    return this.http
      .get<Cinema[]>(url, { headers: this.getHeaders() })
      .pipe(
        map((response) => {
          return response;
        }),
        catchError(this.handleError)
      );
  }  

  getCities(): Observable<string[]> {
    const url = `${this.cinemasApiUrl}/cities`;
    return this.http
      .get<GetCitiesResponse>(url, { headers: this.getHeaders() })
      .pipe(
        map((response) => 
          response.cities.filter((city) => city)
        ),
        catchError(this.handleError)
      );
  }

  getTheaterSeats(cinemaId: number, theaterId: number): Observable<Seat[]> {
    const url = `${this.cinemasApiUrl}/${cinemaId}/theaters/${theaterId}/seats`;
    return this.http
      .get<Seat[]>(url, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Nieznany błąd!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Błąd: ${error.error.message}`;
    } else {
      errorMessage = `Błąd ${error.status}: ${error.error.detail || error.message}`;
    }
    return throwError(errorMessage);
  }
}