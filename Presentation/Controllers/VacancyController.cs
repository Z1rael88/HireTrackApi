using Application.Dtos;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
   // [Authorize(Roles = $"{nameof(Role.HrManager)},{nameof(Role.CompanyAdministrator)}")]
    [Route("api/vacancies")]
    [ApiController]
    public class VacancyController(IVacancyService vacancyService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateVacancy([FromBody] VacancyDto vacancyDto)
        {
            var vacancy = await vacancyService.CreateVacancyAsync(vacancyDto);
            return Ok(vacancy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacancy([FromBody] VacancyDto updateVacancyDto)
        {
            var vacancy = await vacancyService.UpdateVacancyAsync(updateVacancyDto);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            await vacancyService.DeleteVacancyAsync(id);
            return NoContent();
        }
    }
}