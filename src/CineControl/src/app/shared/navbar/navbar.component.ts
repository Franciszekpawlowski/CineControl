// src/app/shared/navbar/navbar.component.ts
import { Component, HostListener } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatIconModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
})
export class NavbarComponent {
  searchQuery: string = '';
  isLoggedIn: boolean = false;
  isMobile: boolean = false;
  isMenuOpen: boolean = false;

  constructor(private authService: AuthService) {
    this.isLoggedIn = this.authService.isAuthenticated();
    this.checkScreenWidth();
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.checkScreenWidth();
  }

  checkScreenWidth() {
    this.isMobile = window.innerWidth <= 768;
    if (!this.isMobile) {
      this.isMenuOpen = false; // Zamknij menu, gdy ekran jest większy
    }
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  onSearch() {
    // Implementacja logiki wyszukiwania filmów
    console.log('Search Query:', this.searchQuery);
    // Przykładowa nawigacja:
    // this.router.navigate(['/search'], { queryParams: { q: this.searchQuery } });
  }
  logout() {
    this.authService.logout();
    this.isLoggedIn = false;
    // Opcjonalnie, przekierowanie po wylogowaniu
    // this.router.navigate(['/']);
  }
}
