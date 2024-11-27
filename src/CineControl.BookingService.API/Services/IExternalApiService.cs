using BookingService.API.Models;
using System.Threading.Tasks;

namespace BookingService.API.Services
{
    public interface IExternalApiService
    {
        Task<SeanceDto> GetSeanceAsync(int seanceId);
        Task<Theater> GetTheaterAsync(int theaterId);
    }
}
