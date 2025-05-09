using Application.Dtos.Resume.Education;
using Application.Dtos.Resume.JobExperience;

namespace Application.Dtos.Resume;

public class ResumeResponseDto 
{
    public int Id { get; set; }
    public int? CandidateId { get; set; }
    public CandidateDto Candidate { get; set; }
    public int YearsOfExperience { get; set; }
    public ICollection<LanguageLevelDto> LanguageLevels { get; set; } 
    public ICollection<JobExperienceResponseDto> JobExperiences { get; set; } 
    public ICollection<EducationResponseDto> Educations { get; set; } 
}