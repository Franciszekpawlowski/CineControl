import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Cinema } from '../models/cinema.model';

@Injectable({
  providedIn: 'root',
})
export class CinemaService {
  private apiUrl = '/Cinemas'; 

  constructor(private http: HttpClient) {}

  getCinemasByCity(city: string): Observable<Cinema[]> {
    return this.http.get<Cinema[]>(`${this.apiUrl}/bycity/${city}`);
  }

  getCities(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/cities`);
  }
}
