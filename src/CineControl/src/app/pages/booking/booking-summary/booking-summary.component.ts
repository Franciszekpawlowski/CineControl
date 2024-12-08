import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../../services/booking.service';
import { Seat } from '../../../models/seat.model';
import { AuthService } from '../../../services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { AuthDialogComponent } from '../../../shared/auth-dialog/auth-dialog.component';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

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

@Component({
  selector: 'app-booking-summary',
  templateUrl: './booking-summary.component.html',
  styleUrls: ['./booking-summary.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    AuthDialogComponent
  ]
})
export class BookingSummaryComponent implements OnChanges {
  @Input() selectedSeatIds: number[] = [];
  @Input() seats: Seat[] = [];
  @Input() seanceId: number | null = null;

  totalPrice: number = 0;
  isReservationInProgress: boolean = false;
  reservationSuccess: boolean = false;
  reservationError: string | null = null;

  constructor(
    private bookingService: BookingService,
    private authService: AuthService,
    private dialog: MatDialog
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    this.calculateTotalPrice();
  }

  calculateTotalPrice() {
    const pricePerSeat = 20;
    this.totalPrice = this.selectedSeatIds.length * pricePerSeat;
  }

  makeReservation() {
    if (!this.seanceId || this.selectedSeatIds.length === 0) return;

    if (!this.authService.isAuthenticated()) {
      this.openAuthDialog().afterClosed().subscribe((result) => {
        if (this.authService.isAuthenticated()) {
          this.makeReservation();
        }
      });
      return;
    }

    this.isReservationInProgress = true;
    this.reservationSuccess = false;
    this.reservationError = null;

    this.bookingService.postReservation(this.seanceId, this.selectedSeatIds).subscribe({
      next: (response: ReservationResponse) => {
        this.isReservationInProgress = false;
        if (response.isSuccess) {
          this.reservationSuccess = true;
        } else {
          this.reservationError = response.errors && response.errors.length > 0 
            ? response.errors[0].message 
            : 'Wystąpił nieznany błąd podczas rezerwacji.';
        }
      },
      error: (err) => {
        this.isReservationInProgress = false;
        this.reservationError = 'Wystąpił błąd po stronie serwera.';
      }
    });
  }

  openAuthDialog() {
    return this.dialog.open(AuthDialogComponent, {
      width: '400px',
      disableClose: true
    });
  }
}
