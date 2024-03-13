namespace MRA.AssetsManagement.Domain.Common;

public interface IAuditableEntity : IEntity
{
    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime LastModifiedAt { get; set; }
}