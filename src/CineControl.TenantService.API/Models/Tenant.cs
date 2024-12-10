using System.ComponentModel.DataAnnotations;

namespace CineControl.TenantService.API.Models
{
    public class Tenant
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
