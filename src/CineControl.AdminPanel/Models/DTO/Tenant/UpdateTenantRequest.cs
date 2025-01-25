namespace CineControl.AdminPanel.Models.DTO.Tenant;

public class UpdateTenantRequest
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string? Description { get; set; }
}