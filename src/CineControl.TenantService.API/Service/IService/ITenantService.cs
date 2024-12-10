using CineControl.Common.Results;
using CineControl.TenantService.API.Models;

namespace CineControl.TenantService.API.Service.IService
{
    public interface ITenantService
    {
        Task<ResultT<IEnumerable<Tenant>>> GetAllAsync();
        Task<ResultT<Tenant>> GetByIdAsync(Guid id);
        Task<ResultT<Tenant>> CreateAsync(Tenant tenant);
        Task<Result> UpdateAsync(Tenant tenant);
        Task<Result> DeleteAsync(Guid id);
    }
}
