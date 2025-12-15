using Application.Dtos.User;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    private IRepository<User> _userRepository = unitOfWork.Repository<User>();
    private IRepository<Company> _companyRepository = unitOfWork.Repository<Company>();
    private IRepository<Candidate> _candidateRepository = unitOfWork.Repository<Candidate>();

    public async Task<UserWithCompanyResponseDto> GetUserProfileById(int userId)
    {
        var result = new UserWithCompanyResponseDto();

        var userWithRole = await _userRepository.GetUserWithRoleById(userId);
        result.Firstname = userWithRole.User.Firstname;
        result.Lastname = userWithRole.User.Lastname;
        result.Username = userWithRole.User.UserName;
        result.Email = userWithRole.User.Email;
        result.Id = userWithRole.User.Id;
        result.Role = Enum.Parse<Role>(userWithRole.Role);
        result.Age = userWithRole.User.Age;
        if (userWithRole.CompanyId is null)
            return result;

        var company = await _companyRepository.GetByIdAsync((int)userWithRole.CompanyId); 

        result.CompanyId = company.Id;
        result.CompanyName = company.Name;
        result.CompanyDescription = company.Description;
        result.CompanyBusinessDomain = company.BusinessDomain;

        return result;
    }

    public async Task<UserWithCompanyResponseDto> GetUserByCandidateId(int candidateId)
    {
        var candidate = await _candidateRepository.GetByIdAsync(candidateId);
        if (candidate.UserId is null)
        {
            throw new NotFoundException("There's no candidate or user id for candidate is null");
        }

        var user = await GetUserProfileById((int)candidate.UserId);
        return user;
    }
}