using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class OrganizationRepository(RbacDbContext context) : GenericRepository<Organization>(context), IOrganizationRepository
    {
        public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId, CancellationToken token)
        {
            return await _context.Organizations
                .AnyAsync(o => o.Name.Equals(name, StringComparison.OrdinalIgnoreCase) 
                        && (!excludeId.HasValue || o.OrganizationId != excludeId.Value), token);
        }

        public async Task<IEnumerable<Organization>> GetChildOrganizationsAsync(Guid parentId, CancellationToken token)
        {
            return await _context.Organizations
                .Where(o => o.ParentOrganizationId == parentId)
                .ToListAsync();
        }
    }
}
