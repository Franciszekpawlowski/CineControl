using CineControl.Common.Results;
using CineControl.OperatorPanel.Models.DTOs.Seances;

namespace CineControl.OperatorPanel.Service.IService;

public interface ISeanceService
{
    Task<ResultT<IEnumerable<GetSeanceResponse>>> GetSeancesAsync();
    Task<ResultT<GetSeanceResponse>> GetSeancesByIdAsync(int Id);
    Task<Result> AddSeanceAsync(AddSeanceRequest addSeanceRequest);
    Task<Result> UpdateSeanceAsync(int id, UpdateSeanceRequest request);
    Task<Result> DeleteSeanceAsync(int id);
}
