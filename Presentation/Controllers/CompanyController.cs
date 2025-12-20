using Application.Dtos.Company;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("companies/")]
public class CompanyController(ICompanyService companyService) : ControllerBase
{
    [Authorize(Roles = nameof(Role.SystemAdministrator))]
    [HttpPost]
    public async Task<IActionResult> CreateCompany(CompanyRequestDto dto)
    {
        var company = await companyService.CreateCompanyAsync(dto);
        return Ok(company);
    }
    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await companyService.GetCompaniesAsync();
        return Ok(companies);
    }
    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetCompanyById(int companyId)
    {
        var company = await companyService.GetCompanyByIdAsync(companyId);
        return Ok(company);
    }
    [Authorize(Roles = nameof(Role.SystemAdministrator))]
    [HttpDelete]
    public async Task<IActionResult> DeleteCompany(int companyId)
    {
        await companyService.DeleteCompanyAsync(companyId);
        return Ok();
    }
    
}