using BookingService.API.Data;
using BookingService.API.Models;
using BookingService.API.Models.Request;
using BookingService.API.Models.Response;
using BookingService.API.Models.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.API.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReservationService> _logger;

        public ReservationService(
            AppDbContext context,
            ILogger<ReservationService> logger)
        {
            _context = context;

            _logger = logger;
        }

        public async Task<bool> AreSeatsAvailableAsync(int seanceId, List<int> seatIds)
        {
            // Pobierz aktualne rezerwacje dla danego seansu
            var reservedSeats = await _context.Tickets
                .Where(t => t.SeanceId == seanceId && seatIds.Contains(t.SeatId))
                .Select(t => t.SeatId)
                .ToListAsync();

            // Sprawdź, czy żadne z żądanych siedzeń nie są już zarezerwowane
            return !reservedSeats.Any();
        }
        public async Task<List<int>> GetReservedSeatsAsync(int seanceId)
        {
            return await _context.Tickets
                .Where(t => t.SeanceId == seanceId)
                .Select(t => t.SeatId)
                .ToListAsync();
        }

        public async Task<GenericResults<ReservationResponse>> CreateReservationAsync(ReservationRequest request)
        {
            var result = new GenericResults<ReservationResponse>();

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                // Sprawdzenie dostępności siedzeń
                var areAvailable = await AreSeatsAvailableAsync(request.SeanceId, request.SeatIds);
                if (!areAvailable)
                {
                    var reservedSeats = await _context.Tickets
                        .Where(t => t.SeanceId == request.SeanceId && request.SeatIds.Contains(t.SeatId))
                        .Select(t => t.SeatId)
                        .ToListAsync();

                    var unavailableSeats = request.SeatIds.Intersect(reservedSeats).ToList();
                    result.AddError($"Siedzenia o ID {string.Join(", ", unavailableSeats)} są już zarezerwowane.");
                    return result;
                }

                // Utworzenie rezerwacji
                var reservation = new Reservation
                {
                    SeanceId = request.SeanceId,
                    ReservationTime = DateTime.UtcNow,
                    Tickets = request.SeatIds.Select(seatId => new Ticket
                    {
                        SeanceId = request.SeanceId,
                        SeatId = seatId
                    }).ToList()
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                // Przygotowanie odpowiedzi
                var response = new ReservationResponse
                {
                    ReservationId = reservation.Id,
                    SeanceId = reservation.SeanceId,
                    SeatIds = reservation.Tickets.Select(t => t.SeatId).ToList(),
                    ReservationTime = reservation.ReservationTime
                };

                result.SetData(response);
                return result;
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                // Sprawdzenie, czy błąd wynika z naruszenia unikalnego indeksu
                if (dbEx.InnerException != null && dbEx.InnerException.Message.Contains("IX_Ticket_SeanceId_SeatId"))
                {
                    // Pobierz siedzenia, które spowodowały konflikt
                    var conflictingSeats = request.SeatIds.ToList(); // Można bardziej precyzyjnie pobrać ID
                    result.AddError($"Siedzenia o ID {string.Join(", ", conflictingSeats)} są już zarezerwowane.");
                    return result;
                }

                _logger.LogError(dbEx, "Wystąpił błąd podczas tworzenia rezerwacji.");
                result.AddError("Wystąpił błąd podczas tworzenia rezerwacji.");
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Wystąpił błąd podczas tworzenia rezerwacji.");
                result.AddError("Wystąpił błąd podczas tworzenia rezerwacji.");
                return result;
            }
        }

    }
    
}
