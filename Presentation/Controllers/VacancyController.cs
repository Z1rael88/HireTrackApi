using Application.Dtos.Vacancy;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVacancyById(int id)
        {
            var vacancy = await vacancyService.GetVacancyByIdAsync(id);
            return Ok(vacancy);
        }
        [Authorize(Roles = $"{nameof(Role.HrManager)},{nameof(Role.Candidate)}")]
        [HttpGet]
        public async Task<IActionResult> GetVacancies()
        {
            var vacancies = await vacancyService.GetVacanciesAsync();
            return Ok(vacancies);
        }
        [Authorize(Roles = $"{nameof(Role.HrManager)},{nameof(Role.Candidate)}")]
        [HttpGet("byCompanyId/{companyId}")]
        public async Task<IActionResult> GetAllVacanciesByCompanyId(int companyId)
        {
            var vacancies = await vacancyService.GetAllVacanciesByCompanyIdAsync(companyId);
            return Ok(vacancies);
        }
        [Authorize(Roles = nameof(Role.HrManager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            await vacancyService.DeleteVacancyAsync(id);
            return NoContent();
        }
        [Authorize(Roles = nameof(Role.Candidate))]
        [HttpGet("byUserId")]
        public async Task<IActionResult> GetVacanciesByUserId(int userId)
        {
            var result = await vacancyService.GetVacanciesByUserIdAsync(userId);
            return Ok(result);
        }
        [Authorize(Roles = nameof(Role.HrManager))]
        [HttpGet("byHrId")]
        public async Task<IActionResult> GetVacanciesByHrId(int hrId)
        {
            var result = await vacancyService.GetByHrIdAsync(hrId);
            return Ok(result);
        }
        
        [Authorize(Roles = nameof(Role.HrManager))]
        [HttpPut("{vacancyId}")]
        public async Task<IActionResult> UpdateVacancyAsync(VacancyRequestDto vacancy, int vacancyId)
        {
            await vacancyService.UpdateVacancyAsync(vacancy, vacancyId);
            return Ok();
        }
    }
}