using Application.Dtos.User;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginUserDto dto);
    Task<TokenDto> Refresh(string refreshToken);
    Task<UserResponseDto> Register(RegisterUserDto dto);

}