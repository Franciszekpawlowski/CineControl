using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Cinemas;
using CineControl.CinemaService.API.Models.DTOs.Theaters;

namespace CineControl.CinemaService.API.Service
{
    public static class CinemaFactory
    {
        public static Cinema CreateCinema(Guid tenantId, AddCinemaRequest request)
        {
            var cinema = new Cinema
            {
                TenantId = tenantId,
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
            };

            // foreach (var config in request.TheaterConfigs)
            // {
            //     var theater = new Theater
            //     {
            //         TenantId = tenantId,
            //         Name = config.Name ?? "Theater",
            //         SeatingCapacity = config.SeatingCapacity,
            //         Seats = GenerateSeats(tenantId, config.SeatingCapacity, config.SeatsPerRow)
            //     };

            //     cinema.Theaters.Add(theater);
            // }

            return cinema;
        }

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
