using RbacService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IRoleMaskingRuleRepository : IGenericRepository<RoleMaskingRule>
    {
        Task<IEnumerable<RoleMaskingRule>> GetRulesByRoleAsync(Guid roleId);
    }
}
