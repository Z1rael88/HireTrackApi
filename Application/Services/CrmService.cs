using Application.Dtos.Resume.Technology;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class CrmService(IRepository<TechnologyType> technologyTypeRepository) : ICrmService
{
    public async Task<TechnologyTypeResponseDto> CreateTechnologyType(TechnologyTypeRequestDto dto)
    {
        var technologyType = dto.Adapt<TechnologyType>();
        var createdTechnologyType = await technologyTypeRepository.CreateAsync(technologyType);
        return createdTechnologyType.Adapt<TechnologyTypeResponseDto>();
    }

    public async Task<IEnumerable<TechnologyTypeResponseDto>> GetAllTechnologyTypes()
    {
        var technologyTypes = await technologyTypeRepository.GetAllAsync();
        return technologyTypes.Adapt<IEnumerable<TechnologyTypeResponseDto>>();
    }
}