namespace Application.Dtos.User;

public class RegisterUserDto : BaseUserDto
{
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public int? CompanyId { get; set; }
}