// src/app/pages/repertoire/repertoire.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

import { CinemaService } from '../../services/cinema.service';
import { SeanceService } from '../../services/seance.service';

import { Cinema } from '../../models/cinema.model';
import { Seance } from '../../models/seance.model';

import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-repertoire',
  templateUrl: './repertoire.component.html',
  styleUrls: ['./repertoire.component.scss'], // Poprawiona literówka
  standalone: true, // Dodane
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
  ],
})
export class RepertoireComponent implements OnInit {
  scheduleForm: FormGroup;

  cities: string[] = [];
  filteredCities: string[] = [];
  cinemas: Cinema[] = [];
  seances: Seance[] = [];

  constructor(
    private fb: FormBuilder,
    private cinemaService: CinemaService,
    private seanceService: SeanceService
  ) {
    this.scheduleForm = this.fb.group({
      city: [''],
      cinema: [null],
      date: [null],
    });
  }

  ngOnInit(): void {
    // Pobieranie listy miast
    this.cinemaService.getCities().subscribe((cities) => {
      this.cities = cities;
    });

    // Filtracja miast na podstawie wpisywanego tekstu
    this.scheduleForm.get('city')?.valueChanges.subscribe((value) => {
      this.filterCities(value);
    });
  }

  filterCities(value: string) {
    const filterValue = value.toLowerCase();
    this.filteredCities = this.cities.filter((city) =>
      city.toLowerCase().includes(filterValue)
    );

    if (this.cities.includes(value)) {
      this.loadCinemas(value);
    } else {
      this.cinemas = [];
      this.scheduleForm.get('cinema')?.setValue(null);
    }
  }

  onCitySelected(city: string) {
    this.loadCinemas(city);
  }

  loadCinemas(city: string) {
    this.cinemaService.getCinemasByCity(city).subscribe((cinemas) => {
      this.cinemas = cinemas;
    });
  }

  onCinemaSelected() {
    this.scheduleForm.get('date')?.setValue(null);
    this.seances = [];
  }

  onDateSelected() {
    const cinemaId = this.scheduleForm.get('cinema')?.value;
    const date = this.scheduleForm.get('date')?.value;

    if (cinemaId && date) {
      this.loadSeances(cinemaId, date);
    }
  }

  loadSeances(cinemaId: number, date: Date) {
    const formattedDate = date.toISOString().split('T')[0];
    this.seanceService
      .getSeances(cinemaId, formattedDate)
      .subscribe((seances) => {
        this.seances = seances;
      });
  }
}
