import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment.prod';
import { Seance } from '../models/seance.model';

@Injectable({
  providedIn: 'root',
})
export class SeanceService {
  private apiUrl = environment.SeancesApiUrl; 
  private tenantId = environment.tenantId;

  constructor(private http: HttpClient) {}

  // Funkcja tworzy nagłówki z X-TenantId
  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'X-TenantId': this.tenantId,
    });
  }

  // Pobieranie seansów według kina i daty
  getSeances(cinemaId: number, date: string): Observable<Seance[]> {
    const url = `${this.apiUrl}/bycinema/${cinemaId}/date/${date}`;
    return this.http
      .get<Seance[]>(url, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  // Pobieranie szczegółów konkretnego seansu
  getSeanceById(seanceId: number): Observable<Seance> {
    const url = `${this.apiUrl}/${seanceId}`;
    return this.http
      .get<Seance>(url, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  // Obsługa błędów
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Nieznany błąd!';
    if (error.error instanceof ErrorEvent) {
      // Błąd klienta
      errorMessage = `Błąd: ${error.error.message}`;
    } else {
      // Błąd serwera
      errorMessage = `Błąd ${error.status}: ${error.error.detail || error.message}`;
    }
    console.error(errorMessage); // Log błędu
    return throwError(errorMessage);
  }
}
