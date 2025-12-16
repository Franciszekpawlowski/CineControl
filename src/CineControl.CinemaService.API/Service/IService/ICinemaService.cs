using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Cinemas;
using CineControl.Common.Results;

namespace CineControl.CinemaService.API.Service.IService
{
    public interface ICinemaService
    {
        Task<ResultT<IEnumerable<CinemaResponse>>> GetAllCinemas();
        Task<ResultT<CinemaResponse>> GetCinemaById(int id);
        Task<ResultT<GetCinemasByCityResponse>> GetAllCities();
        Task<ResultT<IEnumerable<CinemaResponse>>> GetCinemasByCity(string city);

        Task<Result> AddCinema(AddCinemaRequest request);
        Task<Result> UpdateCinema(UpdateCinemaRequest updatedCinema, int cinemaId);
        Task<Result> DeleteCinema(int id);
    }
}
