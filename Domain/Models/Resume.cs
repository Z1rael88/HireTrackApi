namespace Domain.Models;

public class Resume : BaseEntity
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public int? CandidateId { get; set; }
    public User Candidate { get; set; }
    public string Bio { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public ICollection<ResumeLanguage> ResumeLanguages { get; set; } 
    public ICollection<JobExperience> JobExperiences { get; set; } 
    public ICollection<Education> Educations { get; set; } 
}