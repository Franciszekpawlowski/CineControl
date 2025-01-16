import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../../services/booking.service';
import { switchMap, map, catchError } from 'rxjs/operators';
import { of, forkJoin } from 'rxjs';
import { ReservationResponse } from '../../../models/Response/get-reserved-seats-response';
import { SeanceService } from '../../../services/seance.service';
import { Seance } from '../../../models/seance.model';

interface ReservationWithSeance extends ReservationResponse {
  seanceData?: Seance; 
}

@Component({
  selector: 'app-user-reservations',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-reservations.component.html',
  styleUrls: ['./user-reservations.component.scss'],
})


export class UserReservationsComponent implements OnInit {
  reservations: ReservationWithSeance[] = [];
  isLoading = false;
  error: string | null = null;

  constructor(private bookingService: BookingService, private seanceService: SeanceService) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.bookingService
      .getUserReservations()
      .pipe(
        switchMap((reservations: ReservationResponse[]) => {
          if (reservations.length === 0) {
            return of([]); 
          }
          const requests = reservations.map((reservation) =>
            this.seanceService.getSeanceById(reservation.seanceId).pipe(
              map((seance) => {
                const merged: ReservationWithSeance = {
                  ...reservation,
                  seanceData: seance,
                };
                return merged;
              })
            )
          );
          return forkJoin(requests);
        }),
        catchError((err) => {
          this.error = 'Nie udało się pobrać rezerwacji ani seansów.';
          console.error(err);
          return of([]); 
        })
      )
      .subscribe((reservationsWithSeance: ReservationWithSeance[]) => {
        this.reservations = reservationsWithSeance;
        this.isLoading = false;
      });
  }
}
