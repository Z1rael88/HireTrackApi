namespace Application.Dtos.User;

public class TokenDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime  AccessTokenExpirationTime { get; set; }
    public DateTime  RefreshTokenExpirationTime { get; set; }
}