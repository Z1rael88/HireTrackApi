using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CandidateRepository(ApplicationDbContext dbContext) : Repository<Candidate>(dbContext), ICandidateRepository
{
    public async Task<Candidate?> GetCandidateByEmailAsync(string email)
    {
      return await dbContext.Candidates.FirstOrDefaultAsync(x => x.Email == email) ?? null;
    }
}