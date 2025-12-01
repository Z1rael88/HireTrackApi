using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CandidateRepository(IApplicationDbContext dbContext) : ICandidateRepository
{
    public async Task<Candidate>? GetCandidateByEmailAsync(string email)
    {
      return await dbContext.Candidates.FirstOrDefaultAsync(x => x.Email == email);
    }
}