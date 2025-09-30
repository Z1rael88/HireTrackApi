using Application.Dtos.Company;

namespace Application.Interfaces;

public interface ICompanyService
{
    Task<CompanyResponseDto> CreateCompanyAsync(CompanyRequestDto dto);
    Task<IEnumerable<CompanyResponseDto>> GetCompaniesAsync();
    Task<CompanyResponseDto> GetCompanyByIdAsync(int companyId);
}