import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, forkJoin } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CinemaService } from './cinema.service';
import { ErrorResponse } from '../models/Response/error-response.model';
import { ReservationResponse } from '../models/Response/get-reserved-seats-response';
import { environment } from '../../environments/environment.prod';
import { MyReservations } from '../models/Response/get-my-reservations.mode';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  private seatingApiUrl = environment.SeatingApiUrl; 
  private reservationsApiUrl = environment.ReservationsApiUrl; 
  private tenantId = environment.tenantId; 

  constructor(private http: HttpClient, private cinemaService: CinemaService) {}

  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'X-TenantId': this.tenantId,
    });
  }

  /**
   * Pobiera listę zajętych miejsc dla danego seansu.
   */
  getReservedSeats(seanceId: number): Observable<number[]> {
    const url = `${this.seatingApiUrl}/seance/${seanceId}/reserved-seats`;
    return this.http
      .get<{ seatIds: number[] }>(url, { headers: this.getHeaders() })
      .pipe(
        map((response) => response.seatIds),
        catchError((error: HttpErrorResponse) => this.handleHttpError(error))
      );
  }

  /**
   * Tworzy nową rezerwację dla danego seansu.
   */
  postReservation(seanceId: number, seatIds: number[]): Observable<ReservationResponse> {
    const url = this.reservationsApiUrl;
    const body = { seanceId, seatIds };

    return this.http
      .post<ReservationResponse>(url, body, { headers: this.getHeaders() })
      .pipe(
        catchError((error: HttpErrorResponse) => this.handleHttpError(error))
      );
  }


  getUserReservations(): Observable<ReservationResponse[]> {
    const url = `${this.reservationsApiUrl}/MyReservations`;
    return this.http
      .get<ReservationResponse[]>(url, {
        headers: this.getHeaders(),
      })
      .pipe(
        catchError((error: HttpErrorResponse) => this.handleHttpError(error))
      );
  }  
  cancelUserReservation(reservationId: number): Observable<void> {
    const url = `${this.reservationsApiUrl}/${reservationId}`;
    return this.http
      .delete<void>(url, { headers: this.getHeaders() })
      .pipe(
        catchError((error: HttpErrorResponse) => this.handleHttpError(error))
      );
  }

  /**
   * Łączy dane o miejscach w sali i miejscach zajętych.
   */
  getAgrigatedSeats(seanceId: number, cinemaId: number, theaterId: number): Observable<any[]> {
    const seats$ = this.cinemaService.getTheaterSeats(cinemaId, theaterId);
    const reservedSeats$ = this.getReservedSeats(seanceId);

    return forkJoin([seats$, reservedSeats$]).pipe(
      map(([allSeats, reservedIds]) => {
        const reservedSet = new Set(reservedIds);
        console.log('Reserved Seat IDs:', reservedSet);
        return allSeats.map((seat) => ({
          ...seat,
          isReserved: reservedSet.has(seat.id),
        }));
      }),
      catchError((error: HttpErrorResponse) => this.handleHttpError(error))
    );
  }

  /**
   * Obsługuje błędy HTTP.
   */
  private handleHttpError(error: HttpErrorResponse): Observable<never> {
    if (error.status === 409) {
      const conflictError: ErrorResponse = error.error;
      console.error('Conflict error:', conflictError.detail);
      return throwError(() => new Error(conflictError.detail));
    }
    const errorMessage =
      error.error instanceof ErrorEvent
        ? `Błąd: ${error.error.message}`
        : `Błąd ${error.status}: ${error.error.detail || error.message}`;
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
