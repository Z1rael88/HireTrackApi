namespace Application.Dtos.Vacancy;

public class VacancyResponseDto : VacancyRequestDto
{
    public int Id { get; set; }
    public int HrId { get; set; }
    public string CompanyName { get; set; }
}