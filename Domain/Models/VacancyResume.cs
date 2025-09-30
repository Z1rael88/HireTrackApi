namespace Domain.Models;

public class VacancyResume
{
    public int VacancyId { get; set; }
    public Vacancy Vacancy { get; set; }
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
}