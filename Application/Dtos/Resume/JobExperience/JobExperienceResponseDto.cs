using Application.Dtos.Resume.Technology;

namespace Application.Dtos.Resume.JobExperience;

public class JobExperienceResponseDto
{
    public int Id { get; set; }
    public string NameOfCompany { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public ICollection<TechnologyResponseDto> Technologies{ get; set; }
    public string Description { get; set; } = string.Empty;
}