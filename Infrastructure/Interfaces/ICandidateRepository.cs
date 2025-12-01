using Domain.Models;

namespace Infrastructure.Interfaces;

public interface ICandidateRepository
{
    Task<Candidate>? GetCandidateByEmailAsync(string email);
}