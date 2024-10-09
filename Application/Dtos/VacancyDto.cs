namespace Application.Dtos;

public class VacancyDto 
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }= string.Empty;
    public decimal Salary { get; set; }
    public DateOnly AddDate { get; set; }
    public DateOnly EndDate { get; set; }
}

