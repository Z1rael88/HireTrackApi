namespace Domain.Models;

public class UserWithRole
{
    public User User { get; set; }
    public int? CompanyId { get; set; }
    public string Role { get; set; }
    public int Age { get; set; }
}