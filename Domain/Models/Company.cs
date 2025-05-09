using Domain.Enums;

namespace Domain.Models;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public BusinessDomain BusinessDomain { get; set; }
    public ICollection<Vacancy>? Vacancies { get; set; } = [];
}