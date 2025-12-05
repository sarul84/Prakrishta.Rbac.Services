using RbacService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<IEnumerable<Department>> GetDepartmentsByOrganizationAsync(Guid organizationId);
    }
}
