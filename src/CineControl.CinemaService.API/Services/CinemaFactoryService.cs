using System;
using System.Collections.Generic;
using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.Request.Cinemas;

namespace CineControl.CinemaService.API.Services
{
    public static class CinemaFactory
    {
        public static Cinema CreateCinema(AddCinemaRequest request)
        {
            var cinema = new Cinema
            {
                Name = request.Name ?? "Cinema Complex",
                Address = request.Address ?? "123 Movie Street",
                City = request.City ?? "Film City",
                State = request.State ?? "FS",
                ZipCode = request.ZipCode ?? "12345",
            };

            foreach (var config in request.TheaterConfigs)
            {
                var theater = new Theater
                {
                    Name = config.Name ?? "Theater",
                    SeatingCapacity = config.SeatingCapacity,
                    Seats = GenerateSeats(config.SeatingCapacity, config.SeatsPerRow)
                };

                cinema.Theaters.Add(theater);
            }

            return cinema;
        }

        private static List<Seat> GenerateSeats(int seatingCapacity, int seatsPerRow)
        {
            var seats = new List<Seat>();
            int rows = (int)Math.Ceiling(seatingCapacity / (double)seatsPerRow);
            for (int row = 1; row <= rows; row++)
            {
                for (int number = 1; number <= seatsPerRow && seats.Count < seatingCapacity; number++)
                {
                    seats.Add(new Seat
                    {
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
