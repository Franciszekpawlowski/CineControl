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

@Injectable({
  providedIn: 'root',
})
export class CinemaService {
  private cinemasApiUrl = '/Cinemas'; 
  private theatersApiUrl ='/theaters'


  private tenantId: string = '3fa85f64-5717-4562-b3fc-2c963f66afa6';

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
        map(response => response.cities),
        catchError(this.handleError)
      );
  }

  getTheaterSeats(theaterId: number): Observable<Seat[]> {
    const url = `${this.theatersApiUrl}/${theaterId}/seats`;
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