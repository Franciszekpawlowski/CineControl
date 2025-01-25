namespace CineControl.AdminPanel.Models.DTO.TetantOperator;

public class GetTenantOperatorResponse
{
    public Guid TenantId { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}