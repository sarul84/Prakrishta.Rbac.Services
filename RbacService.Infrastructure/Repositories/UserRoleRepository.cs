using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class UserRoleRepository(RbacDbContext context) : GenericRepository<UserRole>(context), IUserRoleRepository
    {
        public async Task<IEnumerable<UserRole>> GetRolesByUserAsync(Guid userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
        }
    }
}
