using CineControl.CinemaService.API.Models;
using System;
using System.Collections.Generic;

public static class CinemaFactory
{
    private static int _globalSeatId = 1; // Global seat ID to ensure uniqueness

    public static Cinema CreateCinema(string name, string address, string city, string state, string zipCode, List<TheaterConfig> theaterConfigs)
    {
        var cinema = new Cinema
        {
            Id = 1,
            Name = name ?? "Cinema Complex",
            Address = address ?? "123 Movie Street",
            City = city ?? "Film City",
            State = state ?? "FS",
            ZipCode = zipCode ?? "12345",
            Theaters = new List<Theater>()
        };

        int theaterId = 1;
        foreach (var config in theaterConfigs)
        {
            var theater = new Theater
            {
                Id = theaterId++,
                Name = config.Name ?? $"Theater {theaterId}",
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
                    Id = _globalSeatId++, // Ensuring unique ID across all seats
                    Row = row,
                    Number = number,
                    Type = SeatType.Standard // Assigning a default SeatType
                });
            }
        }
        return seats;
    }
}

public class TheaterConfig
{
    public string Name { get; set; }
    public int SeatingCapacity { get; set; } = 75; // Default seating capacity
    public int SeatsPerRow { get; set; } = 15; // Default seats per row
}
