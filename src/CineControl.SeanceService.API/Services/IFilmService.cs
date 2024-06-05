using CineControl.SeanceService.API.Models;
using System.Threading.Tasks;

namespace CineControl.SeanceService.API.Services
{
    public interface IFilmService
    {
        Task<Film> GetFilmById(int id);
    }
}
