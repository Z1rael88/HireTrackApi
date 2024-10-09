using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Vacancy> Vacancies { get; }

    Task<int> SaveChangesAsync();
}