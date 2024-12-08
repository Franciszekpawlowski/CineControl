import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CinemaService } from './cinema.service';
import { forkJoin, map } from 'rxjs';

interface ApiResponse<T> {
  data: T;
  errors: { message: string }[];
  isSuccess: boolean;
}

interface ReservationData {
  reservationId: number;
  seanceId: number;
  seatIds: number[];
  reservationTime: string;
}

type ReservationResponse = ApiResponse<ReservationData>;

type SeatingResponse = ApiResponse<number[]>;

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private seatingApiUrl = '/Seating'; 
  private reservationsApiUrl ='/Reservations';

  constructor(private http: HttpClient, private cinemaService: CinemaService) {}

  /**
   * Pobiera listę zajętych miejsc dla danego seansu.
   * Zwraca obiekt z polem data (number[]) zawierającym listę ID zajętych miejsc.
   */
  getReservedSeats(seanceId: number): Observable<SeatingResponse> {
    return this.http.get<SeatingResponse>(`${this.seatingApiUrl}/seance/${seanceId}/reserved-seats`);
  }

  /**
   * Tworzy nową rezerwację dla danego seansu z określonymi miejscami.
   * Oczekuje seanceId oraz tablicy seatIds.
   * Zwraca obiekt z danymi rezerwacji.
   */
  postReservation(seanceId: number, seatIds: number[]): Observable<ReservationResponse> {
    const body = { seanceId, seatIds };
    return this.http.post<ReservationResponse>(this.reservationsApiUrl, body);
  }

  getAgrigatedSeats(seanceId:number, theaterId:number){
    let seats$ = this.cinemaService.getTheaterSeats(theaterId);
    let reservedSeats$ = this.getReservedSeats(seanceId);
    return forkJoin([seats$, reservedSeats$]).pipe(
      map(([allSeats, reservedResponse]) => {
        const reservedIds = new Set(reservedResponse.data);
        return allSeats.map(seat => ({
          ...seat,
          isReserved: reservedIds.has(seat.id) 
        }));
      })
    );
  }
}
