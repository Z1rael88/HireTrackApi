using Domain.Enums;

namespace Domain.Models;

public class UserWithRole
{
    public User User { get; set; }
    public string Role { get; set; }
}