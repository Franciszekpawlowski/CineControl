using System.Threading.Tasks;
using CineControl.SeanceService.API.Models;

namespace CineControl.SeanceService.API.Services
{
    public interface ITheaterService
    {
        Task<TheaterDto> GetTheaterByIdAsync(int theaterId);
    }
}
