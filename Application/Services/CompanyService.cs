using Application.Dtos.Company;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class CompanyService(IUnitOfWork unitOfWork) : ICompanyService
{
    private readonly IRepository<Company> _repository = unitOfWork.Repository<Company>();

    public async Task<CompanyResponseDto> CreateCompanyAsync(CompanyRequestDto dto)
    {
        var company = dto.Adapt<Company>();
        var createdCompany = await _repository.CreateAsync(company);
        return createdCompany.Adapt<CompanyResponseDto>();
    }

    public async Task<IEnumerable<CompanyResponseDto>> GetCompaniesAsync()
    {
        var companies = await _repository.GetAllAsync();
        return companies.Adapt<IEnumerable<CompanyResponseDto>>();
    }

    public async Task<CompanyResponseDto> GetCompanyByIdAsync(int companyId)
    {
        var company = await _repository.GetByIdAsync(companyId);
        return company.Adapt<CompanyResponseDto>();
    }
}