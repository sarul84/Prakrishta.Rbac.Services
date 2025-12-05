using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Infrastructure.Repositories
{
    public class RoleRepository(RbacDbContext context) : GenericRepository<Role>(context), IRoleRepository
    {
        public async Task<IEnumerable<Role>> GetRolesByApplicationAsync(Guid applicationId)
        {
            return await _context.Roles
                .Where(r => r.ApplicationId == applicationId)
                .ToListAsync();
        }
    }
}
