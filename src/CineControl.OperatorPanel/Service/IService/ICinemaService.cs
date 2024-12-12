using CineControl.Common.Results;
using CineControl.OperatorPanel.Models.DTOs.Cinemas;

namespace CineControl.OperatorPanel.Service.IService;

public interface ICinemaService
{
    Task<ResultT<IEnumerable<GetCinemaResponse>>> GetCinemasAsync();
}
