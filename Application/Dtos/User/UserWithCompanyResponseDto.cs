using Domain.Enums;

namespace Application.Dtos.User;

public class UserWithCompanyResponseDto : UserResponseDto
{
    public string CompanyName { get; set; }
    public string CompanyDescription { get; set; }
    public BusinessDomain CompanyBusinessDomain { get; set; }
}