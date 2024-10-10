// src/app/shared/footer/footer.component.ts
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatToolbarModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
})
export class FooterComponent {
  email: string = '';

  subscribeNewsletter() {
    // Implementacja logiki subskrypcji newslettera
    console.log('Subskrypcja newslettera:', this.email);
    // Możesz dodać tutaj logikę wysyłania e-maila lub integrację z usługą newslettera
    this.email = ''; // Resetowanie pola po subskrypcji
  }
}
