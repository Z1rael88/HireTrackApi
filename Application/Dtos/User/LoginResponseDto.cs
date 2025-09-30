namespace Application.Dtos.User;

public class LoginResponseDto
{
    public TokenDto TokenDto { get; set; }
    public UserResponseDto UserResponseDto { get; set; }
}