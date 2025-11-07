using Application.Dtos.User;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    private IRepository<User> _userRepository = unitOfWork.Repository<User>();
    private IRepository<Company> _companyRepository = unitOfWork.Repository<Company>();

    public async Task<UserWithCompanyResponseDto> GetUserProfileById(int userId)
    {
        var result = new UserWithCompanyResponseDto();
        
        var userWithRole =  await _userRepository.GetUserWithRoleById(userId);
        result.Firstname = userWithRole.User.Firstname;
        result.Lastname = userWithRole.User.Lastname;
        result.Username = userWithRole.User.UserName;
        result.Email = userWithRole.User.Email;
        result.Id = userWithRole.User.Id;
        result.Role = Enum.Parse<Role>(userWithRole.Role);
        if (userWithRole.CompanyId is null) return userWithRole.User.Adapt<UserWithCompanyResponseDto>();
        var company = await _companyRepository.GetByIdAsync((int)userWithRole.CompanyId);//

        result.CompanyId = company.Id;
        result.CompanyName = company.Name;
        result.CompanyDescription = company.Description;
        result.CompanyBusinessDomain = company.BusinessDomain;

        return result;
    }
}