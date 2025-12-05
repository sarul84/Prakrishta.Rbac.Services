using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class EnumerationRepository(RbacDbContext context) : GenericRepository<Enumeration>(context), IEnumerationRepository
    {
        public async Task<IEnumerable<Enumeration>> GetByTypeAsync(string type)
        {
            return await _context.Enumerations
                .Where(e => e.Type == type)
                .ToListAsync();
        }
    }
}
