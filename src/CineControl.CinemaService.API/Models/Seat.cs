namespace CineControl.CinemaService.API.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int TheaterId { get; set; }
        public Guid TenantId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public SeatType Type { get; set; }

        public virtual Theater Theater { get; set; } = null!;
    }

    public enum SeatType
    {
        Standard,
        VIP,
        Disabled
    }
}
