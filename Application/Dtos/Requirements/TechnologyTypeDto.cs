using Domain.Enums;

namespace Application.Dtos.Requirements;

public class TechnologyTypeDto
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public TechnologyCategory TechnologyCategory { get; set; }
}