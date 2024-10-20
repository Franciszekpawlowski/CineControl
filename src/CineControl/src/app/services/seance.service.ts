// src/app/services/seance.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { Seance } from '../models/seance.model';

@Injectable({
  providedIn: 'root',
})
export class SeanceService {
  private apiUrl = environment.seanceApiUrl;
  private seancesUrl = 'seances.json';

  constructor(private http: HttpClient) {}

  /* getSeances(cinemaId: number, date: string): Observable<Seance[]> {
    return this.http.get<Seance[]>(
      `${this.apiUrl}?cinemaId=${cinemaId}&date=${date}`
    );
  } */
  getSeances(cinemaId: number, date: string): Observable<Seance[]> {
    return this.http.get<Seance[]>(this.seancesUrl).pipe(
      map((seances) =>
        seances.filter(
          (seance) =>
            seance.cinemaId === cinemaId &&
            seance.startTime.startsWith(date)
        )
      )
    );
  }
}
