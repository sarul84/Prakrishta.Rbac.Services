using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class RoleMaskingRuleRepository(RbacDbContext context) : GenericRepository<RoleMaskingRule>(context), IRoleMaskingRuleRepository
    {
        public async Task<IEnumerable<RoleMaskingRule>> GetRulesByRoleAsync(Guid roleId)
        {
            return await _context.RoleMaskingRules
                .Where(rmr => rmr.RoleId == roleId)
                .ToListAsync();
        }
    }
}
