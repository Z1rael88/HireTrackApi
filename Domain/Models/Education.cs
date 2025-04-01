using Domain.Enums;

namespace Domain.Models;

public class Education : BaseEntity
{
    public string Title { get; set; }
    public EducationType EducationType { get; set; }
    public Degree? Degree { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Description { get; set; }
    public int ResumeId { get; set; }
}