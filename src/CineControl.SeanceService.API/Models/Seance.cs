using System;

namespace CineControl.SeanceService.API.Models
{
    public class Seance
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; } 
        public int MovieId { get; set; }
        public int TheaterId { get; set; }
        public int CinemaId { get; set; }

        private DateTime startTime;
        private DateTime endTime;

        public DateTime StartTime
        {
            get => startTime;
            set => startTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public DateTime EndTime
        {
            get => endTime;
            set => endTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public Movie Movie { get; set; }
    }
}