// src/app/services/cinema.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


import { Cinema } from '../models/cinema.model';

@Injectable({
  providedIn: 'root',
})
export class CinemaService {
  private apiUrl = 'https://twoje-api.com/api/cinemas';
  private cinemasUrl = 'cinemas.json';

  constructor(private http: HttpClient) {}

  /* getCinemasByCity(city: string): Observable<Cinema[]> {
    return this.http.get<Cinema[]>(`${this.apiUrl}?city=${city}`);
  }

  getCities(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/cities`);
  } */
  getCinemasByCity(city: string): Observable<Cinema[]> {
    return this.http.get<Cinema[]>(this.cinemasUrl).pipe(
      // Filtracja kin na podstawie miasta
      map((cinemas) => cinemas.filter((cinema) => cinema.city.toLowerCase() === city.toLowerCase()))
    );
  }

  getCities(): Observable<string[]> {

    return this.http.get<Cinema[]>(this.cinemasUrl).pipe(
       map((cinemas) => Array.from(new Set(cinemas.map(cinema => cinema.city))))
     );
  }
}
