using Domain.Enums;

namespace Domain.Models;

public class TechnologyType : BaseEntity
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public TechnologyCategory TechnologyCategory { get; set; }
}