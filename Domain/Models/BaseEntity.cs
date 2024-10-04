using Infrastructure.Interfaces;

namespace Domain.Models;

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public Guid UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }
}