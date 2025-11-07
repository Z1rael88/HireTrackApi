using Domain.Enums;

namespace Application.Dtos.User;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public Role Role { get; set; }
    public int? CompanyId { get; set; }
}