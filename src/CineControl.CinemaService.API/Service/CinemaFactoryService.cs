using CineControl.CinemaService.API.Models;

namespace CineControl.CinemaService.API.Service
{
    public static class CinemaFactory
    {
        public static List<Seat> GenerateSeats(Guid tenantId,int cinemaId, int seatingCapacity, int seatsPerRow)
        {
            var seats = new List<Seat>();
            int rows = (int)Math.Ceiling(seatingCapacity / (double)seatsPerRow);
            for (int row = 1; row <= rows; row++)
            {
                for (int number = 1; number <= seatsPerRow && seats.Count < seatingCapacity; number++)
                {
                    seats.Add(new Seat
                    {
                        TheaterId = cinemaId,
                        TenantId = tenantId,
                        Row = row,
                        Number = number,
                        Type = SeatType.Standard
                    });
                }
            }
            return seats;
        }
    }
}
