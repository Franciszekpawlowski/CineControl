import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SeanceService } from '../../services/seance.service';
import { BookingService } from '../../services/booking.service';
import { Seance } from '../../models/seance.model';
import { Seat } from '../../models/seat.model';
import { CommonModule } from '@angular/common';
import { BookingSummaryComponent } from './booking-summary/booking-summary.component';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.scss'],
  standalone: true,
  imports: [CommonModule, BookingSummaryComponent]
})
export class BookingComponent implements OnInit {
  seance: Seance | null = null;
  seats: Seat[] = []; 
  seatRows: Seat[][] = [];
  selectedSeatIds: number[] = [];

  constructor(
    private route: ActivatedRoute, 
    private seanceService: SeanceService, 
    private bookingService: BookingService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
  
    this.seanceService.getSeanceById(id).subscribe({
      next: (data) => {
        this.seance = data;
        this.bookingService.getAgrigatedSeats(this.seance.id, this.seance.theaterId).subscribe({
          next: (seatsData) => {
            this.seats = seatsData;
            this.organizeSeatsByRow();
          },
          error: (err) => console.error('Błąd podczas pobierania siedzeń', err)
        });
      },
      error: (err) => console.error('Błąd podczas pobierania seansu', err)
    });
  }

  organizeSeatsByRow() {
    if (!this.seats) return;
    const rowsMap = new Map<number, Seat[]>();

    for (const seat of this.seats) {
      if (!rowsMap.has(seat.row)) {
        rowsMap.set(seat.row, []);
      }
      rowsMap.get(seat.row)!.push(seat);
    }

    const sortedRows = Array.from(rowsMap.keys()).sort((a, b) => a - b);
    this.seatRows = sortedRows.map(row => {
      const rowSeats = rowsMap.get(row)!;
      rowSeats.sort((a, b) => a.number - b.number);
      return rowSeats;
    });
  }

  toggleSeat(seat: Seat) {
    if (seat.isReserved) return;
  
    const index = this.selectedSeatIds.indexOf(seat.id);
    if (index > -1) {
      this.selectedSeatIds = [
        ...this.selectedSeatIds.slice(0, index),
        ...this.selectedSeatIds.slice(index + 1)
      ];
    } else {
      this.selectedSeatIds = [...this.selectedSeatIds, seat.id];
    }
  }
  

  isSelected(seat: Seat): boolean {
    return this.selectedSeatIds.includes(seat.id);
  }

  getRowLabel(rowNumber: number): string {
    // Konwertowanie numer rzędu (1-based) na literę: 1->A, 2->B...
    const charCode = 'A'.charCodeAt(0) + (rowNumber - 1);
    return String.fromCharCode(charCode);
  }
}
