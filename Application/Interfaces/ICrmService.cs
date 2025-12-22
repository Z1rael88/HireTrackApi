using Application.Dtos.Resume.Technology;

namespace Application.Interfaces;

public interface ICrmService
{
    Task<TechnologyTypeResponseDto> CreateTechnologyType(TechnologyTypeRequestDto dto);
    Task<IEnumerable<TechnologyTypeResponseDto>> GetAllTechnologyTypes();
}