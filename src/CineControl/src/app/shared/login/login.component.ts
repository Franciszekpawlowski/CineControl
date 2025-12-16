import { Component, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
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
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  @Output() loginSuccess = new EventEmitter<void>();

  constructor(
    private authService: AuthService, 
    private router: Router,
    private dialogRef: MatDialogRef<any>
  ) {}

  login() {
    if (!this.email || !this.password) {
      this.errorMessage = 'Please fill in all fields.';
      return;
    }

    this.authService
      .login({ email: this.email, password: this.password })
      .subscribe({
        next: () => {
          this.errorMessage = '';
          this.loginSuccess.emit();
          this.dialogRef.close(); 
          // this.router.navigate(['/']);
        },
        error: () => {
          this.errorMessage = 'Invalid login credentials.';
        },
      });
  }
}
