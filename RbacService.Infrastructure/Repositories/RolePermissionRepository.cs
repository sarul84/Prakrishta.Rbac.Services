using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class RolePermissionRepository(RbacDbContext context) : GenericRepository<RolePermission>(context), IRolePermissionRepository
    {
        public async Task<IEnumerable<RolePermission>> GetPermissionsByRoleAsync(Guid roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync();
        }
    }
}
