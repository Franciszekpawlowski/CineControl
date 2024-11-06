import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Seance } from '../models/seance.model';

@Injectable({
  providedIn: 'root',
})
export class SeanceService {
  private apiUrl = '/api/Seances'; // Używamy względnego URL dla proxy

  constructor(private http: HttpClient) {}

  getSeances(cinemaId: number, date: string): Observable<Seance[]> {
    return this.http.get<Seance[]>(`${this.apiUrl}/bycinema/${cinemaId}/date/${date}`);
  }
}
