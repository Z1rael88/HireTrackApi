
namespace Application.Dtos.Company;

public class CompanyResponseDto : CompanyRequestDto
{
    public int Id { get; set; }
    public ICollection<Domain.Models.Vacancy> Vacancies { get; set; } = [];
}