// geo.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GeoService {
  constructor(private http: HttpClient) {}

  getCityFromCoords(lat: number, lon: number): Observable<string> {
    const url = `https://nominatim.openstreetmap.org/reverse?lat=${lat}&lon=${lon}&format=json`;
    return this.http.get<any>(url).pipe(
      map((data) => {
        return data?.address?.city || data?.address?.town || 'Nieznane';
      })
    );
  }
}
