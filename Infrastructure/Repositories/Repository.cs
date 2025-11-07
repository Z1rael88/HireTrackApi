using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDbContext dbContext) : IRepository<T>
        where T : class, IBaseEntity
    {
        protected readonly DbSet<T> Entities = dbContext.Set<T>();
        protected readonly ApplicationDbContext DbContext = dbContext;

        public async Task<T> CreateAsync(T entity)
        {
            var newEntity = await Entities.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return newEntity.Entity;
        }

        public IQueryable<T> Query()
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var updatedEntity = Entities.Update(entity);
            await DbContext.SaveChangesAsync();
            return updatedEntity.Entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = Entities;
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await Entities
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with {id} Id not found");
            }

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException();
            }

            await UpdateAsync(entity);
        }

        public async Task<UserWithRole> GetUserWithRoleById(int userId)
        {
            var user = await GetUserById(userId);
            var role = await GetUserRoleNameAsync(userId);
            return new UserWithRole
            {
                Role = role,
                User = user
            };
        }

        public async Task<User> GetUserById(int id)
        {
            return await dbContext.Users.Where(x => x.Id == id).SingleOrDefaultAsync() ??
                   throw new NotFoundException($"User with id: {id} not found");
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        private async Task<string?> GetUserRoleNameAsync(int userId)
        {
            return await DbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Join(
                    DbContext.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => r.Name)
                .SingleOrDefaultAsync();
        }
    }
}