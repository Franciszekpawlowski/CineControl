using CineControl.Common.Results;
using CineControl.OperatorPanel.Models.DTOs.Cinemas;

namespace CineControl.OperatorPanel.Service.IService;

public interface ICinemaService
{
    Task<ResultT<IEnumerable<GetCinemaResponse>>> GetCinemasAsync();
    Task<ResultT<GetCinemaResponse>> GetCinemasByIdAsync(int id);
    Task<Result> AddCinemaAsync(AddCinemaRequest request);
    Task<Result> UpdateCinemaAsync(int id, UpdateCinemaRequest request);
    Task<Result> DeleteCinemaAsync(int id);
}
