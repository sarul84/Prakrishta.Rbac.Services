using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Interfaces.Repositories
{

    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Remove(T entity);
        void Delete(T entity);
        void Restore(T entity);
    }
}
