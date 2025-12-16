namespace CineControl.AdminPanel.Models.DTO.Tenant;

public class GetTenantResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}