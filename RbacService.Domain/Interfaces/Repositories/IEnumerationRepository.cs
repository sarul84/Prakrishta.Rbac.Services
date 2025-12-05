using RbacService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IEnumerationRepository : IGenericRepository<Enumeration>
    {
        Task<IEnumerable<Enumeration>> GetByTypeAsync(string type);
    }
}
