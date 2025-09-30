using Application.Dtos.Resume.Technology;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class CrmService(IUnitOfWork unitOfWork) : ICrmService
{
    private readonly IRepository<TechnologyType> _repository = unitOfWork.Repository<TechnologyType>();
    public async Task<TechnologyTypeResponseDto> CreateTechnologyType(TechnologyTypeRequestDto dto)
    {
        var technologyType = dto.Adapt<TechnologyType>();
        var createdTechnologyType = await _repository.CreateAsync(technologyType);
        return createdTechnologyType.Adapt<TechnologyTypeResponseDto>();
    }

    public async Task<IEnumerable<TechnologyTypeResponseDto>> GetAllTechnologyTypes()
    {
        var technologyTypes = await _repository.GetAllAsync();
        return technologyTypes.Adapt<IEnumerable<TechnologyTypeResponseDto>>();
    }
}