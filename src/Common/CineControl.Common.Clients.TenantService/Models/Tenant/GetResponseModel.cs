namespace CineControl.Common.Clients.TenantService.Models.Tenant;

public class GetTenantResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}