import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Promotion } from '../models/promotion.model';
import { Movie } from '../models/movie.model';
import { environment } from '../../environments/environment.prod';
@Injectable({
  providedIn: 'root',
})
export class MovieService {
  private apiUrl = environment.seanceApiUrl;

  constructor(private http: HttpClient) {}

  getCurrentMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(`${this.apiUrl}/current`);
  }

  getUpcomingMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(`${this.apiUrl}/upcoming`);
  }

  getTopRatedMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(`${this.apiUrl}/top-rated`);
  }

  getMovieDetails(id: number): Observable<Movie> {
    return this.http.get<Movie>(`${this.apiUrl}/${id}`);
  }

  getPersonalizedRecommendations(userId: number): Observable<Movie[]> {
    return this.http.get<Movie[]>(`${this.apiUrl}/recommendations/${userId}`);
  }
  getSpecialEvents(): Observable<Promotion[]> {
    return this.http.get<Promotion[]>(`${this.apiUrl}/special-events`);
  }
  getMovieById(id: number): Observable<Movie> {
    return this.http.get<Movie>(`${this.apiUrl}/${id}`);
  }
}
