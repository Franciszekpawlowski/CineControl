import { Component, OnInit } from '@angular/core';
import { Promotion } from '../../models/promotion.model';
import { Movie } from '../../models/movie.model';
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
  currentMovies: Movie[] = [
    {
      id: 1,
      title: 'Film Aktualny 1',
      description: 'Długi opis filmu aktualnego 1. Akcja, przygoda i dramat w jednym...',
      shortDescription: 'Krótki opis filmu aktualnego 1',
      duration: 120,
      genre: 'Akcja, Dramat',
      posterUrl: 'images/movie1.jpg',
      releaseDate: '2023-10-01',
      rating: 4.5,
    },
    {
      id: 2,
      title: 'Film Aktualny 2',
      description: 'Film opowiada o grupie przyjaciół, którzy wyruszają na niezapomnianą przygodę...',
      shortDescription: 'Krótki opis filmu aktualnego 2',
      duration: 110,
      genre: 'Przygoda, Fantasy',
      posterUrl: 'images/movie2.jpg',
      releaseDate: '2023-10-10',
      rating: 4.0,
    },
    {
      id: 3,
      title: 'Film Aktualny 3',
      description: 'Film detektywistyczny pełen zwrotów akcji...',
      shortDescription: 'Krótki opis filmu aktualnego 3',
      duration: 130,
      genre: 'Kryminał, Thriller',
      posterUrl: 'images/movie3.webp',
      releaseDate: '2023-10-15',
      rating: 4.8,
    },
    {
      id: 4,
      title: 'Film Aktualny 1',
      description: 'Długi opis filmu aktualnego 1. Akcja, przygoda i dramat w jednym...',
      shortDescription: 'Krótki opis filmu aktualnego 1',
      duration: 120,
      genre: 'Akcja, Dramat',
      posterUrl: 'images/movie1.jpg',
      releaseDate: '2023-10-01',
      rating: 4.5,
    },
    {
      id: 5,
      title: 'Film Aktualny 2',
      description: 'Film opowiada o grupie przyjaciół, którzy wyruszają na niezapomnianą przygodę...',
      shortDescription: 'Krótki opis filmu aktualnego 2',
      duration: 110,
      genre: 'Przygoda, Fantasy',
      posterUrl: 'images/movie2.jpg',
      releaseDate: '2023-10-10',
      rating: 4.0,
    },
    {
      id: 6,
      title: 'Film Aktualny 3',
      description: 'Film detektywistyczny pełen zwrotów akcji...',
      shortDescription: 'Krótki opis filmu aktualnego 3',
      duration: 130,
      genre: 'Kryminał, Thriller',
      posterUrl: 'images/movie3.webp',
      releaseDate: '2023-10-15',
      rating: 4.8,
    },
    {
      id: 7,
      title: 'Film Aktualny 3',
      description: 'Film detektywistyczny pełen zwrotów akcji...',
      shortDescription: 'Krótki opis filmu aktualnego 3',
      duration: 130,
      genre: 'Kryminał, Thriller',
      posterUrl: 'images/movie3.webp',
      releaseDate: '2023-10-15',
      rating: 4.8,
    },
  ];

  upcomingMovies: Movie[] = [
    {
      id: 4,
      title: 'Film Nadchodzący 1',
      description: 'Nadchodzący hit kina. Historia miłosna...',
      shortDescription: 'Krótki opis filmu nadchodzącego 1',
      duration: 125,
      genre: 'Romans, Sci-Fi',
      posterUrl: 'images/movie1.jpg',
      releaseDate: '2023-12-01',
      rating: 4.6,
    },
    {
      id: 5,
      title: 'Film Nadchodzący 2',
      description: 'Film przygodowy dla całej rodziny...',
      shortDescription: 'Krótki opis filmu nadchodzącego 2',
      duration: 115,
      genre: 'Przygoda, Familijny',
      posterUrl: 'images/movie2.jpg',
      releaseDate: '2023-12-10',
      rating: 4.3,
    },
    {
      id: 6,
      title: 'Film Nadchodzący 3',
      description: 'Film akcji z elementami fantastyki...',
      shortDescription: 'Krótki opis filmu nadchodzącego 3',
      duration: 140,
      genre: 'Akcja, Fantasy',
      posterUrl: 'images/movie3.webp',
      releaseDate: '2023-12-20',
      rating: 4.7,
    },
  ];

  personalizedMovies: Movie[] = [
    {
      id: 7,
      title: 'Film Polecany 1',
      description: 'Film polecany specjalnie dla Ciebie...',
      shortDescription: 'Krótki opis filmu polecanego 1',
      duration: 130,
      genre: 'Dramat, Thriller',
      posterUrl: 'images/movie1.jpg',
      releaseDate: '2023-11-15',
      rating: 4.9,
    },
    {
      id: 8,
      title: 'Film Polecany 2',
      description: 'Kolejny film, który może Cię zainteresować...',
      shortDescription: 'Krótki opis filmu polecanego 2',
      duration: 100,
      genre: 'Komedia, Akcja',
      posterUrl: 'images/movie2.jpg',
      releaseDate: '2023-11-20',
      rating: 4.2,
    },
  ];

  specialEvents: Promotion[] = [
    {
      id: 3,
      title: 'Wydarzenie Specjalne 1',
      description: 'Opis wydarzenia specjalnego 1',
      imageUrl: 'images/movie1.jpg',
      ctaText: 'Zobacz',
      link: '/events/1',
    },
  ];

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
      imageUrl: 'images/movie1.jpg',
      ctaText: 'Dowiedz się więcej',
      link: '/promotions/2',
    },
  ];

  constructor() {}

  ngOnInit() {
    // Kod inicjalizacyjny, jeśli potrzebny
  }
}
