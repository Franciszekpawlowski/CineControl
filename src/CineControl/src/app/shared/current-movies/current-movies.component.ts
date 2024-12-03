import { Component, OnInit } from '@angular/core';
import { Movie } from '../../models/movie.model';
import { MovieService } from '../../services/movie.service';
import { MovieSliderComponent } from '../movie-slider/movie-slider.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-current-movies',
  templateUrl: './current-movies.component.html',
  styleUrls: ['./current-movies.component.scss'],
  standalone: true,
  imports: [CommonModule, MovieSliderComponent],
})
export class CurrentMoviesComponent implements OnInit {
  currentMovies: Movie[] = [];

  constructor(private movieService: MovieService) {}

  ngOnInit() {
    this.movieService.getCurrentMovies().subscribe(
      (movies) => (this.currentMovies = movies || []),
      (error) => {
        console.error('Błąd przy pobieraniu aktualnych filmów', error);
        this.currentMovies = [];
      }
    );
  }
}
