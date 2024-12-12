using CineControl.Common.Results;
using CineControl.TenantService.API.Data;
using CineControl.TenantService.API.Models;
using CineControl.TenantService.API.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace CineControl.TenantService.API.Service
{
    public class TenantService : ITenantService
    {
        private readonly TenantDbContext _context;

        public TenantService(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<ResultT<IEnumerable<Tenant>>> GetAllAsync()
        {
            var tenants = await _context.Tenants.ToListAsync();
            return tenants;
        }

        public async Task<ResultT<Tenant>> GetByIdAsync(Guid id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            return tenant is not null ? tenant : Error.NotFound("Tenant not found");
        }

        public async Task<ResultT<Tenant>> CreateAsync(Tenant tenant)
        {
            //TODO validacja
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
            return tenant;
        }

        public async Task<Result> UpdateAsync(Tenant tenant)
        {
            var existing = await _context.Tenants.FindAsync(tenant.Id);
            if (existing is null)
            {
                return Error.NotFound("Tenant not found");
            }

            existing.Name = tenant.Name;
            existing.Description = tenant.Description;
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant is null)
            {
                return Error.NotFound("Tenant not found");
            }

            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
