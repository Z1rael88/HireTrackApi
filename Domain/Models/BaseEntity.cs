using Domain.Interfaces;

namespace Domain.Models;

public class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
}