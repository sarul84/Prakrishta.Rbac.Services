using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class OrgAccessMappingRepository(RbacDbContext context) : GenericRepository<OrgAccessMapping>(context), IOrgAccessMappingRepository
    {
        public async Task<IEnumerable<OrgAccessMapping>> GetMappingsBySourceOrgAsync(Guid sourceOrgId)
        {
            return await _context.OrgAccessMappings
                .Where(mapping => mapping.SourceOrganizationId == sourceOrgId)
                .ToListAsync();
        }
    }
}
