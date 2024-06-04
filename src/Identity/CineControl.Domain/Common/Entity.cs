using System.ComponentModel.DataAnnotations;

namespace CineControl.Domain.Common;

public abstract class Entity
{

    [Key]
    public Guid Id { get; set; }
}
