using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class PermissionRepository(RbacDbContext context) : GenericRepository<Permission>(context), IPermissionRepository
    {
        public async Task<IEnumerable<Permission>> GetPermissionsByResourceAsync(string resource)
        {
            return await _context.Permissions
                .Where(p => p.Resource == resource)
                .ToListAsync();
        }
    }
}
