using Domain.Interfaces;

namespace Domain.Models;

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}