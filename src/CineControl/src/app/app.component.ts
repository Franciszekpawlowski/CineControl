import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { FooterComponent } from './shared/footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HomeComponent, NavbarComponent, FooterComponent],
  template: '<app-navbar></app-navbar><router-outlet></router-outlet><app-footer></app-footer>',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'cinema-control';
}
