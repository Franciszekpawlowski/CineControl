using CineControl.BookingService.API.Data;
using CineControl.BookingService.API.Models;
using CineControl.BookingService.API.Models.DTOs.Reservations;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using CineControl.BookingService.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineControl.BookingService.API.Services
{
    public class ReservationService : IReservationService 
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReservationService> _logger;
        private readonly ITenantProvider _tenantProvider;

        public ReservationService(
            AppDbContext context,
            ILogger<ReservationService> logger,
            ITenantProvider tenantProvider)
        {
            _context = context;
            _logger = logger;
            _tenantProvider = tenantProvider;
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

        public async Task<ResultT<List<int>>> GetReservedSeatsAsync(int seanceId)
        {
            var seats = await _context.Tickets
                .Where(t => t.SeanceId == seanceId)
                .Select(t => t.SeatId)
                .ToListAsync();

            return seats;
        }

        public async Task<ResultT<ReservationResponse>> CreateReservationAsync(ReservationRequest request)
        {
            if (!_tenantProvider.HasTenant())
            {
                return BookingErrors.AccessUnauthorized("Brak określonego tenant.");
            }

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
                    return BookingErrors.Conflict($"Siedzenia o ID {string.Join(", ", unavailableSeats)} są już zarezerwowane.");
                }

                // Utworzenie rezerwacji
                var reservation = new Reservation
                {
                    TenantId = _tenantProvider.GetTenantId(),
                    SeanceId = request.SeanceId,
                    ReservationTime = DateTime.UtcNow,
                    Tickets = request.SeatIds.Select(seatId => new Ticket
                    {
                        TenantId = _tenantProvider.GetTenantId(),
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

                return response; 
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                // Sprawdzenie, czy błąd wynika z naruszenia unikalnego indeksu
                if (dbEx.InnerException != null && dbEx.InnerException.Message.Contains("IX_Ticket_SeanceId_SeatId"))
                {
                    // Pobierz siedzenia, które spowodowały konflikt
                    var conflictingSeats = request.SeatIds.ToList(); // Można bardziej precyzyjnie pobrać ID
                    return BookingErrors.Conflict($"Siedzenia o ID {string.Join(", ", conflictingSeats)} są już zarezerwowane.");
                }

                _logger.LogError(dbEx, "Wystąpił błąd podczas tworzenia rezerwacji.");
                return BookingErrors.Failure("Wystąpił błąd podczas tworzenia rezerwacji.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Wystąpił błąd podczas tworzenia rezerwacji.");
                return BookingErrors.Failure("Wystąpił błąd podczas tworzenia rezerwacji.");
            }
        }

    }
}
