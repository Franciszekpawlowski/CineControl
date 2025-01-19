using CineControl.Common.Results;
using CineControl.OperatorPanel.Models.DTOs.Theaters;

namespace CineControl.OperatorPanel.Service.IService;

public interface ITheaterService {
    Task<Result> AddTheaterAsync(int cinemaId, AddTheaterRequest request);
    Task<ResultT<GetTheaterResponse>> GetTheaterByIdAsync(int cinemaId, Guid id);
    Task<ResultT<IEnumerable<GetTheaterResponse>>> GetTheatersAsync(int cinemaId);
    Task<Result> UpdateTheaterAsync(int cinemaId, Guid id, UpdateTheaterRequest request);
}