using RbacService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        Task<IEnumerable<UserRole>> GetRolesByUserAsync(Guid userId);
    }
}
