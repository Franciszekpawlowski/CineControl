namespace CineControl.Domain;

public abstract class AuditEntity
{
    public DateTime created { get; set; }
    public string createdBy { get; set; }
    public DateTime lastModified { get; set; }
    public string lastModifiedBy { get; set; }
}
