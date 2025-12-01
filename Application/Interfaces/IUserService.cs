using Application.Dtos.User;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserWithCompanyResponseDto> GetUserProfileById(int userId);
    Task<UserWithCompanyResponseDto> GetUserByCandidateId(int candidateId);
}