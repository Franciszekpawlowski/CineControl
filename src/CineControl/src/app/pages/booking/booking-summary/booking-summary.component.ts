import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../../services/booking.service';
import { Seat } from '../../../models/seat.model';

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
  imports: [CommonModule]
})
export class BookingSummaryComponent implements OnChanges {
  @Input() selectedSeatIds: number[] = [];
  @Input() seats: Seat[] = [];
  @Input() seanceId: number | null = null;

  totalPrice: number = 0;
  isReservationInProgress: boolean = false;
  reservationSuccess: boolean = false;
  reservationError: string | null = null;

  constructor(private bookingService: BookingService) {}

  ngOnChanges(changes: SimpleChanges): void {
    this.calculateTotalPrice();
  }

  calculateTotalPrice() {
    const pricePerSeat = 20;
    this.totalPrice = this.selectedSeatIds.length * pricePerSeat;
  }

  makeReservation() {
    if (!this.seanceId || this.selectedSeatIds.length === 0) return;

    this.isReservationInProgress = true;
    this.reservationSuccess = false;
    this.reservationError = null;

    this.bookingService.postReservation(this.seanceId, this.selectedSeatIds).subscribe({
      next: (response: ReservationResponse) => {
        this.isReservationInProgress = false;
        if (response.isSuccess) {
          this.reservationSuccess = true;
          // Opcjonalnie: możesz emitować zdarzenie lub odświeżyć listę siedzeń
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
}
