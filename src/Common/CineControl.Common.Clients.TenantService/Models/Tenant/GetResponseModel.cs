namespace CineControl.Common.Clients.TenantService.Models.Tenant;

public class GetResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}