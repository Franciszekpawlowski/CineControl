using System;

namespace CineControl.SeanceService.API.Models.DTOs.Seances
{
    public class SeanceCreateDto
    {
        public int MovieId { get; set; }
        public int TheaterId { get; set; }
        public int CinemaId { get; set; }
        public DateTime StartTime { get; set; }
    }
}