namespace Infrastructure.Interfaces;

public interface IBaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public Guid UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }
}