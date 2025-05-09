using Domain.Enums;

namespace Application.Dtos.Company;

public class CompanyRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public BusinessDomain BusinessDomain { get; set; }
}