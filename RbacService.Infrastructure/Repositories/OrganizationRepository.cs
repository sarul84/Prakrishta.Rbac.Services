using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class OrganizationRepository(RbacDbContext context) : GenericRepository<Organization>(context), IOrganizationRepository
    {
        public async Task<IEnumerable<Organization>> GetChildOrganizationsAsync(Guid parentId)
        {
            return await _context.Organizations
                .Where(o => o.ParentOrganizationId == parentId)
                .ToListAsync();
        }
    }
}
