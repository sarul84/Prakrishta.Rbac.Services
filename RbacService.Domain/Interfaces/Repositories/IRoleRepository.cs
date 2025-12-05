using RbacService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<IEnumerable<Role>> GetRolesByApplicationAsync(Guid applicationId);
    }
}
