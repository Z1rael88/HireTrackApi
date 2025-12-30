using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos.User;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using Infrastructure.Exceptions;
using Infrastructure.ValidationOptions;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthService(
    UserManager<User> userManager,
    IOptions<JwtOptions> jwtOptions,
    ICandidateRepository candidateRepository,
    IRepository<User> userRepository,
    RoleManager<IdentityRole<int>> roleManager,
    IValidator<BaseUserDto> validator)
    : IAuthService
{
    public async Task<LoginResponseDto> Login(LoginUserDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            throw new NotFoundException($"User with that email is not found");
        }

        bool isPasswordValid = await userManager.CheckPasswordAsync(user, dto.Password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }

        var userId = user.Id;
        var role = await GetRoleByUserAsync(user);
        var accessToken = GenerateAccessToken(userId, role);
        var refreshToken = GenerateRefreshToken(userId);
        var userProfileWithRole = await userRepository.GetUserWithRoleById(userId);
        var result = new LoginResponseDto()
        {
            UserResponseDto = userProfileWithRole.Adapt<UserResponseDto>(),
            TokenDto = CreateTokensDto(accessToken, refreshToken)
        };
        return result;
    }

    public async Task<LoginResponseDto> Refresh(string refreshToken)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        TokenValidationParameters validationParameters = new()
        {
            ValidIssuer = jwtOptions.Value.Issuer,
            ValidAudience = jwtOptions.Value.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey)),
        };
        var principal = tokenHandler.ValidateToken(
            refreshToken,
            validationParameters,
            out SecurityToken? _);
        var userIdClaim = principal.FindFirst(ClaimTypes.Sid)?.Value;
        if (userIdClaim == null)
        {
            throw new SecurityTokenException("Invalid token");
        }

        if (!int.TryParse(userIdClaim, out int userId))
        {
            throw new SecurityTokenException("Invalid user profile ID format");
        }

        var user = await userRepository.GetByIdAsync(userId);
        var role = await GetRoleByUserAsync(user);
        var newAccessToken = GenerateAccessToken(userId, role);
        var newRefreshToken = GenerateRefreshToken(userId);
        var userProfileWithRole = await userRepository.GetUserWithRoleById(user.Id);
        var result = new LoginResponseDto()
        {
            UserResponseDto = userProfileWithRole.Adapt<UserResponseDto>(),
            TokenDto = CreateTokensDto(newAccessToken, newRefreshToken)
        };
        return result;
    }


    public async Task<UserResponseDto> Register(RegisterUserDto dto)
    {
        validator.ValidateAndThrow(dto);
        var user = dto.Adapt<User>();
        var roleName = dto.Role.ToString();
        await ValidateRoleAsync(dto.Role);
        await CreateUserAndAssignRoleAsync(user, dto.Password, roleName);
        var resultDto = user.Adapt<UserResponseDto>();
        resultDto.Role = dto.Role;

        var candidate = await candidateRepository.GetCandidateByEmailAsync(user.Email!);
        if (candidate is not null)
        {
            candidate.UserId = user.Id;
            await candidateRepository.SaveChangesAsync();
        }

        return resultDto;
    }

    private async Task ValidateRoleAsync(Role role)
    {
        if (!await roleManager.RoleExistsAsync(role.ToString()) && role == Role.SystemAdministrator)
        {
            throw new NotFoundException(
                $"That Role with name {role.ToString()} is not found");
        }
    }

    private async Task CreateUserAndAssignRoleAsync(User user, string password, string roleName)
    {
        var userCreationResult = await userManager.CreateAsync(user, password);
        if (!userCreationResult.Succeeded)
        {
            throw new IdentityException("User creation failed", userCreationResult.Errors);
        }

        var roleAssignmentResult = await userManager.AddToRoleAsync(user, roleName);
        if (!roleAssignmentResult.Succeeded)
        {
            throw new IdentityException("Role assignment failed", roleAssignmentResult.Errors);
        }
    }

    private TokenDto CreateTokensDto(string accessToken, string refreshToken)
    {
        var refreshTokenExpiryTime = jwtOptions.Value.RefreshTokenExpiryMinutes;
        var accessTokenExpiryTime = jwtOptions.Value.AccessTokenExpiryMinutes;
        var accessTokenExpirationDate = DateTime.UtcNow.AddMinutes(accessTokenExpiryTime);
        var refreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(refreshTokenExpiryTime);
        return new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpirationTime = refreshTokenExpirationDate,
            AccessTokenExpirationTime = accessTokenExpirationDate,
        };
    }

    private async Task<string> GetRoleByUserAsync(User user)
    {
        var roles = await userManager.GetRolesAsync(user);
        return roles.Single();
    }

    private string GenerateAccessToken(int userId, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, userId.ToString()),
            new(ClaimTypes.Role, role),
        };
        var expirationTime = DateTime.UtcNow.AddMinutes(jwtOptions.Value.AccessTokenExpiryMinutes);
        return GenerateToken(claims, expirationTime);
    }

    private string GenerateRefreshToken(int userId)
    {
        var claims = new List<Claim> { new(ClaimTypes.Sid, userId.ToString()) };
        var expirationTime = DateTime.UtcNow.AddMinutes(jwtOptions.Value.RefreshTokenExpiryMinutes);
        return GenerateToken(claims, expirationTime);
    }

    private string GenerateToken(
        IEnumerable<Claim> claims,
        DateTime expiration)
    {
        var jwtOptionsValue = jwtOptions.Value;

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionsValue.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            jwtOptionsValue.Issuer,
            jwtOptionsValue.Audience,
            claims,
            DateTime.UtcNow,
            expiration,
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}