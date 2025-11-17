using Application.Dtos.Resume.Education;
using Application.Dtos.Resume.JobExperience;

namespace Application.Dtos.Resume;

public class ResumeRequestDto
{
    public CandidateDto Candidate { get; set; }
    public int ExpectedSalary { get; set; }
    public int YearsOfExperience { get; set; }
    public ICollection<LanguageLevelDto> LanguageLevels { get; set; } 
    public ICollection<JobExperienceRequestDto> JobExperiences { get; set; } 
    public ICollection<EducationRequestDto> Educations { get; set; } 
    public int? VacancyId { get; set; }
}