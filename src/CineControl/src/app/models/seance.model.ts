// src/app/models/seance.model.ts
export interface Seance {
    id: number;
    cinemaId: number;
    movieId: number;
    movieTitle: string;
    startTime: string; // lub Date, jeśli wolisz
    endTime: string;
  }
  