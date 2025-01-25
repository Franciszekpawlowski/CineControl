using System.ComponentModel.DataAnnotations;

namespace CineControl.AdminPanel.Models.DTO.TetantOperator;

public class CreateOperatorRequest
{
    public Guid TenantId { get; set; }
    public string Username { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}