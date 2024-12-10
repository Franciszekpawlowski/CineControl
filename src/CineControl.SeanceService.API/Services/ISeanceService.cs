using CineControl.SeanceService.API.Models;
using CineControl.SeanceService.API.Models.DTOs.Seances;
using CineControl.Common.Results;

namespace CineControl.SeanceService.API.Service.IService
{
    public interface ISeanceService
    {
        Task<ResultT<IEnumerable<Seance>>> GetAllSeances();
        Task<ResultT<Seance>> GetSeanceById(int id);
        Task<ResultT<IEnumerable<Seance>>> GetSeancesByCinemaAndDate(int cinemaId, DateTime date);
        Task<ResultT<Seance>> AddSeance(SeanceCreateDto seanceCreateDto);
        Task<Result> UpdateSeance(int id, SeanceDto seanceDto);
        Task<Result> DeleteSeance(int id);
    }
}