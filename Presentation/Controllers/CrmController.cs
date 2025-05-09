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