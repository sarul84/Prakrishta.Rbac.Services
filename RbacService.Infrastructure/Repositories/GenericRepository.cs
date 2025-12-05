using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly RbacDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(RbacDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) => await _dbSet.ToListAsync();
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.IsDeleted = true;
                baseEntity.DeletedAt = DateTime.UtcNow;
                _context.Update(entity);
            }
            else
            {
                throw new InvalidOperationException("Entity does not support soft delete.");
            }
        }
        public void Remove(T entity) => _dbSet.Remove(entity);
        public void Restore(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.IsDeleted = false;
                baseEntity.DeletedAt = null;
                _context.Update(entity);
            }
        }
    }
}
