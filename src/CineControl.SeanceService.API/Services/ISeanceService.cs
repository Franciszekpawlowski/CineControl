using CineControl.SeanceService.API.Models;
using CineControl.SeanceService.API.Models.DTOs.Seances;
using CineControl.Common.Results;

namespace CineControl.SeanceService.API.Service.IService
{
    public interface ISeanceService
    {
        Task<ResultT<IEnumerable<SeanceDto>>> GetAllSeances();
        Task<ResultT<SeanceDto>> GetSeanceById(int id);
        Task<ResultT<IEnumerable<SeanceDto>>> GetSeancesByCinemaAndDate(int cinemaId, DateTime date);
        Task<ResultT<SeanceDto>> AddSeance(SeanceCreateDto seanceCreateDto);
        Task<Result> UpdateSeance(int id, SeanceDto seanceDto);
        Task<Result> DeleteSeance(int id);
        Task<ResultT<IEnumerable<SeanceDto>>> GetSeancesByCinema(int cinemaId);
    }
}