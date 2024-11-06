import { Component, OnInit } from '@angular/core';
import { Promotion } from '../../models/promotion.model';
import { Movie } from '../../models/movie.model';
import { MovieService } from '../../services/movie.service';
import { MatCardModule } from '@angular/material/card';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CarouselComponent } from '../../shared/carousel/carousel.component';
import { MovieSliderComponent } from '../../shared/movie-slider/movie-slider.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    RouterModule,
    CarouselComponent,
    MovieSliderComponent,
  ],
})
export class HomeComponent implements OnInit {
  currentMovies: Movie[] = [];
  upcomingMovies: Movie[] = [];
  personalizedMovies: Movie[] = [];
  promotions: Promotion[] = [
    {
      id: 1,
      title: 'Promocja 1',
      description: 'Opis promocji 1',
      imageUrl: 'images/movie1.jpg',
      ctaText: 'Sprawdź',
      link: '/promotions/1',
    },
    {
      id: 2,
      title: 'Promocja 2',
      description: 'Opis promocji 2',
      imageUrl: 'images/movie2.jpg',
      ctaText: 'Dowiedz się więcej',
      link: '/promotions/2',
    },
  ];

  constructor(private movieService: MovieService) {}

  ngOnInit() {
    this.movieService.getCurrentMovies().subscribe(
      (movies) => {
        this.currentMovies = movies || [];
      },
      (error) => {
        console.error('Błąd przy pobieraniu aktualnych filmów', error);
        this.currentMovies = []; 
      }
    );

    this.movieService.getUpcomingMovies().subscribe(
      (movies) => {
        this.upcomingMovies = movies || []; 
      },
      (error) => {
        console.error('Błąd przy pobieraniu nadchodzących filmów', error);
        this.upcomingMovies = [];
      }
    );

    const userId = 1; 
    this.movieService.getPersonalizedRecommendations(userId).subscribe(
      (movies) => {
        this.personalizedMovies = movies || []; 
      },
      (error) => {
        console.error('Błąd przy pobieraniu rekomendacji', error);
        this.personalizedMovies = []; 
      }
    );
  }
}