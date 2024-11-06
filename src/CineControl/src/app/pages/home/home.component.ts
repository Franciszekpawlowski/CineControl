import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { PromotionsComponent } from '../../shared/promotions/promotions.component';
import { CurrentMoviesComponent } from '../../shared/current-movies/current-movies.component';
import { UpcomingMoviesComponent } from '../../shared/upcoming-movies/upcoming-movies.component';
import { PersonalizedMoviesComponent } from '../../shared/personalized-movies/personalized-movies.component';

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
    PromotionsComponent,
    CurrentMoviesComponent,
    UpcomingMoviesComponent,
    PersonalizedMoviesComponent,
  ],
})
export class HomeComponent {}
