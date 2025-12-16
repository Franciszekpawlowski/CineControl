import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Promotion {
  id: number;
  title: string;
  description: string;
  imageUrl: string;
  ctaText: string;
  link: string;
}

@Injectable({
  providedIn: 'root',
})
export class PromotionService {
  private apiUrl = 'https://api.twojekino.pl/promotions';

  constructor(private http: HttpClient) {}

  getPromotions(): Observable<Promotion[]> {
    return this.http.get<Promotion[]>(this.apiUrl);
  }

  getSpecialEvents(): Observable<Promotion[]> {
    return this.http.get<Promotion[]>(`${this.apiUrl}/special-events`);
  }
}
