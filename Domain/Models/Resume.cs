using Domain.Enums;

namespace Domain.Models;

public class Resume : BaseEntity
{
    public int? CandidateId { get; set; }
    public Candidate Candidate { get; set; }
    public int YearsOfExperience { get; set; }
    public ICollection<LanguageLevel> LanguageLevels { get; set; } 
    public ICollection<JobExperience> JobExperiences { get; set; } 
    public ICollection<Education> Educations { get; set; }
    public ICollection<VacancyResume> VacancyResumes { get; set; }
}