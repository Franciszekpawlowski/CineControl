using System.ComponentModel.DataAnnotations;

namespace CineControl.TenantService.API.Models
{
    public class Tenant
    {
        private Guid _id;
        [Key]
        public Guid Id
        {
            get => _id;
            set{_id = (value == Guid.Empty) ? Guid.NewGuid() : value;}
        }
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

    }
}
