using Application.Dtos.Resume.Technology;

namespace Application.Dtos.Resume.JobExperience;

public class JobExperienceRequestDto
{
    public string NameOfCompany { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public ICollection<TechnologyRequestDto> Technologies { get; set; }
    public string Description { get; set; } = string.Empty;
}