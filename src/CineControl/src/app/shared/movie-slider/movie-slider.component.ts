import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Movie } from '../../models/movie.model';
import { MatCardModule } from '@angular/material/card';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-movie-slider',
  standalone: true,
  imports: [CommonModule, MatCardModule, RouterModule], // Dodane moduły
  templateUrl: './movie-slider.component.html',
  styleUrls: ['./movie-slider.component.scss'], // Poprawiona literówka
})
export class MovieSliderComponent {
  @Input() movies: Movie[] = [];
}
