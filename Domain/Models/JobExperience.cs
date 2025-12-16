namespace Domain.Models;

public class JobExperience : BaseEntity
{
    public string NameOfCompany { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public ICollection<Technology> Technologies { get; set; }
    public string Description { get; set; } = string.Empty;
    
    public int ResumeId { get; set; }
}