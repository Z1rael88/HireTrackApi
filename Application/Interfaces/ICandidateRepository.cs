using Domain.Models;

namespace Application.Interfaces;

public interface ICandidateRepository
{
    Task<Candidate?> GetCandidateByEmailAsync(string email);
}