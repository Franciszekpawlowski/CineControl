import { Component, OnInit } from '@angular/core';
import { Movie } from '../../models/movie.model';
import { MovieService } from '../../services/movie.service';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { CarouselComponent } from '../carousel/carousel.component';

@Component({
  selector: 'app-personalized-movies',
  templateUrl: './personalized-movies.component.html',
  styleUrls: ['./personalized-movies.component.scss'],
  standalone: true,
  imports: [CommonModule, MatButtonModule, RouterModule, MatIconModule, CarouselComponent],
})
export class PersonalizedMoviesComponent implements OnInit {
  personalizedMovies: Movie[] = [];
  userId = 1; // Możesz dynamicznie pobierać ID użytkownika, jeśli jest dostępne

  constructor(private movieService: MovieService) {}

  ngOnInit() {
    this.movieService.getPersonalizedRecommendations(this.userId).subscribe(
      (movies) => (this.personalizedMovies = movies || []),
      (error) => {
        console.error('Błąd przy pobieraniu rekomendacji', error);
        this.personalizedMovies = [];
      }
    );
  }
}
