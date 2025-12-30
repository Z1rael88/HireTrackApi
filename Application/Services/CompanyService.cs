using Application.Dtos.Company;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class CompanyService(IRepository<Company> companyRepository) : ICompanyService
{

    public async Task<CompanyResponseDto> CreateCompanyAsync(CompanyRequestDto dto)
    {
        var company = dto.Adapt<Company>();
        var createdCompany = await companyRepository.CreateAsync(company);
        return createdCompany.Adapt<CompanyResponseDto>();
    }

    public async Task<IEnumerable<CompanyResponseDto>> GetCompaniesAsync()
    {
        var companies = await companyRepository.GetAllAsync();
        return companies.Adapt<IEnumerable<CompanyResponseDto>>();
    }

    public async Task<CompanyResponseDto> GetCompanyByIdAsync(int companyId)
    {
        var company = await companyRepository.GetByIdAsync(companyId);
        return company.Adapt<CompanyResponseDto>();
    }

    public async Task DeleteCompanyAsync(int companyId)
    {
        await companyRepository.DeleteAsync(companyId);
    }
}