using Domain.Enums;
namespace Application.Dtos.User;

public abstract class BaseUserDto
{
    public required string FirstName { get; set; } 
    public required string LastName { get; set; } 
    public required string Email { get; set; } 
    public required string Username { get; set; } 
    public required int Age { get; set; }
    public Role Role { get; set; }
}