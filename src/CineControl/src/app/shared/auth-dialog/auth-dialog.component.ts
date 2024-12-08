import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';
import { RegistrationComponent } from '../registration/registration.component';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-auth-dialog',
  templateUrl: './auth-dialog.component.html',
  styleUrls: ['./auth-dialog.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    MatTabsModule,
    LoginComponent,
    RegistrationComponent,
    MatButtonModule
  ]
})
export class AuthDialogComponent {
  constructor(private dialogRef: MatDialogRef<AuthDialogComponent>) {}

  closeDialog() {
    this.dialogRef.close(); 
  }
}
