using Domain.Enums;

namespace Application.Dtos.Resume.Technology;

public class TechnologyTypeRequestDto
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public TechnologyCategory TechnologyCategory { get; set; }
}