import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Seance } from '../models/seance.model';

@Injectable({
  providedIn: 'root',
})
export class SeanceService {
  private apiUrl = '/Seances'; 

  constructor(private http: HttpClient) {}

  getSeances(cinemaId: number, date: string): Observable<Seance[]> {
    return this.http.get<Seance[]>(`${this.apiUrl}/bycinema/${cinemaId}/date/${date}`);
  }
  getSeanceById(seanceId:number): Observable<Seance>{
    return this.http.get<Seance>(`${this.apiUrl}/${seanceId}`);
  }
}
