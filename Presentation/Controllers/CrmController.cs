using Application.Dtos.Resume.Technology;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("crm/")]
public class CrmController(ICrmService crmService) : ControllerBase
{
    [HttpGet("languages")]
    public IActionResult GetAllLanguages()
    {
        var languages = Enum.GetNames(typeof(Language));
        return Ok(languages);
    }
    [HttpGet("languageLevels")]
    public IActionResult GetAllLanguageLevels()
    {
        var languageLevels = Enum.GetNames(typeof(LanguageLevel));
        return Ok(languageLevels);
    }
    [HttpGet("degrees")]
    public IActionResult GetAllDegrees()
    {
        var degrees = Enum.GetNames(typeof(Degree));
        return Ok(degrees);
    }
    [HttpGet("educationTypes")]
    public IActionResult GetAllEducationTypes()
    {
        var educationTypes = Enum.GetNames(typeof(EducationType));
        return Ok(educationTypes);
    }
    [HttpGet("technologyCategories")]
    public IActionResult GetAllTechCategories()
    {
        var techCategories = Enum.GetNames(typeof(TechnologyCategory));
        return Ok(techCategories);
    }

    [HttpPost("technologyTypes")]
    public async Task<IActionResult> CreateTechnologyType(TechnologyTypeRequestDto dto)
    {
        var technologyType = await crmService.CreateTechnologyType(dto);
        return Ok(technologyType);
    }
    [HttpGet("technologyTypes")]
    public async Task<IActionResult> GetAllTechnologyTypes()
    {
        var technologyTypes = await crmService.GetAllTechnologyTypes();
        return Ok(technologyTypes);
    }
}