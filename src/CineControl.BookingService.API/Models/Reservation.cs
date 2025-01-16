using System;
using System.Collections.Generic;

namespace CineControl.BookingService.API.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public int SeanceId { get; set; }
        public string UserId { get; set; }
        public DateTime ReservationTime { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
