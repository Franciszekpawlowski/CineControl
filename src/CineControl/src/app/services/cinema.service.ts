import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Seat } from '../models/seat.model';

import { Cinema } from '../models/cinema.model';

@Injectable({
  providedIn: 'root',
})
export class CinemaService {
  private cinemasApiUrl = '/Cinemas'; 
  private theatersApiUrl ='/theaters'

  constructor(private http: HttpClient) {}

  getCinemasByCity(city: string): Observable<Cinema[]> {
    return this.http.get<Cinema[]>(`${this.cinemasApiUrl}/bycity/${city}`);
  }

  getCities(): Observable<string[]> {
    return this.http.get<string[]>(`${this.cinemasApiUrl}/cities`);
  }
  
  getTheaterSeats(theaterId:number): Observable<Seat[]>{
    return this.http.get<Seat[]>(`${this.theatersApiUrl}/${theaterId}/seats`);
  }
}
