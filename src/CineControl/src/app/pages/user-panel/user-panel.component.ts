import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { GetUserResponseModel } from '../../models/Response/get-user-response.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-panel',
  imports: [CommonModule],
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.scss'],
  standalone: true,
})
export class UserPanelComponent implements OnInit {
  user!: GetUserResponseModel; 
  isLoading: boolean = true; 
  error: string | null = null; 

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.getUser().subscribe({
      next: (data) => {
        this.user = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Nie udało się załadować danych użytkownika.';
        console.error(err);
        this.isLoading = false;
      },
    });
  }
}
