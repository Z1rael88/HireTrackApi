using Domain.Models;

namespace Application.Interfaces;

public interface ICandidateRepository : IRepository<Candidate>
{
    Task<Candidate?> GetCandidateByEmailAsync(string email);
}