import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

import { PromotionsComponent } from '../../shared/promotions/promotions.component';
import { CinemaService } from '../../services/cinema.service';
import { SeanceService } from '../../services/seance.service';
import { GeoService } from '../../services/geo.service'; 

import { Cinema } from '../../models/cinema.model';
import { Seance } from '../../models/seance.model';

interface ShowTime {
  time: string;
  seanceId: number;
}

interface GroupedSeances {
  movieTitle: string;
  posterB64: string;
  shows: ShowTime[];
}

@Component({
  selector: 'app-repertoire',
  templateUrl: './repertoire.component.html',
  styleUrls: ['./repertoire.component.scss'], 
  standalone: true, 
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    MatOptionModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatCardModule,
    PromotionsComponent,
  ],
})
export class RepertoireComponent implements OnInit {
  scheduleForm: FormGroup;

  cities: string[] = [];
  filteredCities: string[] = [];
  cinemas: Cinema[] = [];
  groupedSeances: GroupedSeances[] = []; 

  constructor(
    private fb: FormBuilder,
    private cinemaService: CinemaService,
    private seanceService: SeanceService,
    private router: Router,
    private geoService: GeoService 
  ) {
    this.scheduleForm = this.fb.group({
      city: [''],
      cinema: [null],
      date: [null],
    });
  }

  ngOnInit(): void {
    this.cinemaService.getCities().subscribe((cities) => {
      this.cities = cities;
      this.filteredCities = cities.slice(); 
      navigator.geolocation.getCurrentPosition(
        (position) => {
          const { latitude, longitude } = position.coords;
          
          this.geoService.getCityFromCoords(latitude, longitude).subscribe(
            (cityName) => {
              if (this.cities.includes(cityName)) {
                this.setCityAndLoadCinemas(cityName);
              } else {
                const fallbackCity = this.cities[0];
                this.setCityAndLoadCinemas(fallbackCity);
              }
            },
            (err) => {
              const fallbackCity = this.cities[0];
              this.setCityAndLoadCinemas(fallbackCity);
            }
          );
        },
        (error) => {
          const fallbackCity = this.cities[0];
          this.setCityAndLoadCinemas(fallbackCity);
        }
      );
    });

    this.scheduleForm.get('city')?.valueChanges.subscribe((value) => {
      this.filterCities(value);
    });
  }

  private setCityAndLoadCinemas(cityName: string) {
    this.scheduleForm.get('city')?.setValue(cityName);
    this.loadCinemas(cityName, true); 
  }

  filterCities(value: string) {
    const filterValue = value.toLowerCase();
    this.filteredCities = this.cities.filter((city) =>
      city.toLowerCase().includes(filterValue)
    );

    if (!this.cities.includes(value)) {
      this.cinemas = [];
      this.scheduleForm.get('cinema')?.setValue(null);
    }
  }

  onCitySelected(city: string) {
    this.loadCinemas(city);
  }

  loadCinemas(city: string, autoSelectFirst = false) {
    this.cinemaService.getCinemasByCity(city).subscribe({
      next: (cinemas) => {
        this.cinemas = cinemas;
        console.log("Cinemas loaded:", this.cinemas); 

        if (autoSelectFirst && this.cinemas.length > 0) {
          this.scheduleForm.get('cinema')?.setValue(this.cinemas[0].id);
          this.scheduleForm.get('date')?.setValue(new Date());
        }
      },
      error: (err) => {
        console.error("Error loading cinemas:", err);
        this.cinemas = [];
      },
    });
  }

  onCinemaSelected() {
    // Gdy user zmieni kino manualnie, czyścimy datę i seanse
    this.scheduleForm.get('date')?.setValue(null);
    this.groupedSeances = [];
  }

  onDateSelected() {
    const cinemaId = this.scheduleForm.get('cinema')?.value;
    const date = this.scheduleForm.get('date')?.value;
  
    if (cinemaId && date) {
      this.loadSeances(cinemaId, date);
    }
  }

  loadSeances(cinemaId: number, date: Date) {
    const utcDate = new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()));
    const formattedDate = utcDate.toISOString().split('T')[0];
  
    this.seanceService.getSeances(cinemaId, formattedDate).subscribe(
      (seances) => {
        this.groupSeancesByMovie(seances);
      },
      (error) => console.error("Błąd przy pobieraniu seansów:", error)
    );
  }

  groupSeancesByMovie(seances: Seance[]) {
    const grouped = seances.reduce((acc: GroupedSeances[], seance) => {
      const movieGroup = acc.find(g => g.movieTitle === seance.movieTitle);
      const timeStr = new Date(seance.startTime).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
      const showTime: ShowTime = { time: timeStr, seanceId: seance.id };
  
      if (movieGroup) {
        movieGroup.shows.push(showTime);
      } else {
        acc.push({
          movieTitle: seance.movieTitle,
          posterB64: seance.psterBase64,
          shows: [showTime]
        });
      }
  
      return acc;
    }, []);
    this.groupedSeances = grouped;
  }

  goToBooking(seanceId: number) {
    this.router.navigate(['/booking', seanceId]);
  }
}
