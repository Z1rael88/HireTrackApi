using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class UserRepository(IApplicationDbContext dbContext) : IUserRepository
{
}