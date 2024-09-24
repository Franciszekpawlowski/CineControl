namespace CineControl.CinemaService.API.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public SeatType Type { get; set; }
    }

    public enum SeatType
    {
        Standard,
        VIP,
        Disabled
    }
}
