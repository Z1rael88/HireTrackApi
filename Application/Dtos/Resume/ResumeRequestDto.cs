using Application.Dtos.Resume.Education;
using Application.Dtos.Resume.JobExperience;

namespace Application.Dtos.Resume;

public class ResumeRequestDto
{
    public CandidateDto Candidate { get; set; }
    public string Bio { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public ICollection<LanguageLevelDto> LanguageLevels { get; set; } 
    public ICollection<JobExperienceRequestDto> JobExperiences { get; set; } 
    public ICollection<EducationRequestDto> Educations { get; set; } 
}