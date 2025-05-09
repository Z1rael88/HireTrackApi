namespace Domain.Models;

public class Vacancy : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }= string.Empty;
    public string CompanyName { get; set; }
    public decimal Salary { get; set; }
    public DateOnly AddDate { get; set; }
    public DateOnly EndDate { get; set; }
    public User Hr { get; set; }
    public int HrId { get; set; }
    public Company Company { get; set; }
    public int CompanyId { get; set; }
    public ICollection<VacancyResume> VacancyResumes { get; set; }
}