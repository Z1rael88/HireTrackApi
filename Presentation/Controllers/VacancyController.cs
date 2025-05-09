using Application.Dtos;
using Application.Dtos.Vacancy;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
   // [Authorize(Roles = $"{nameof(Role.HrManager)},{nameof(Role.CompanyAdministrator)}")]
    [Route("vacancies")]
    [ApiController]
    public class VacancyController(IVacancyService vacancyService) : ControllerBase
    {
        [Authorize(Roles = nameof(Role.HrManager))]
        [HttpPost]
        public async Task<IActionResult> CreateVacancy([FromBody] VacancyRequestDto vacancyRequestDto)
        {
            var vacancy = await vacancyService.CreateVacancyAsync(vacancyRequestDto);
            return Ok(vacancy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacancy([FromBody] VacancyRequestDto updateVacancyRequestDto)
        {
            var vacancy = await vacancyService.UpdateVacancyAsync(updateVacancyRequestDto);
            return Ok(vacancy);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVacancyById(int id)
        {
            var vacancy = await vacancyService.GetVacancyByIdAsync(id);
            return Ok(vacancy);
        }

        [HttpGet]
        public async Task<IActionResult> GetVacancies()
        {
            var vacancies = await vacancyService.GetVacanciesAsync();
            return Ok(vacancies);
        }

        [HttpGet("byCompanyId/{companyId}")]
        public async Task<IActionResult> GetAllVacanciesByCompanyId(int companyId)
        {
            var vacancies = await vacancyService.GetAllVacanciesByCompanyIdAsync(companyId);
            return Ok(vacancies);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            await vacancyService.DeleteVacancyAsync(id);
            return NoContent();
        }
    }
}