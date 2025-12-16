import { Component, OnInit } from '@angular/core';
import { Movie } from '../../models/movie.model';
import { MovieService } from '../../services/movie.service';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { CarouselComponent } from '../carousel/carousel.component';
@Component({
  selector: 'app-upcoming-movies',
  templateUrl: './upcoming-movies.component.html',
  styleUrls: ['./upcoming-movies.component.scss'],
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, CarouselComponent,RouterModule],
})
export class UpcomingMoviesComponent implements OnInit {
  upcomingMovies: Movie[] = [];

  constructor(private movieService: MovieService) {}

  ngOnInit() {
    this.movieService.getUpcomingMovies().subscribe(
      (movies) => (this.upcomingMovies = movies || []),
      (error) => {
        console.error('Błąd przy pobieraniu nadchodzących filmów', error);
        this.upcomingMovies = [];
      }
    );
  }
}
