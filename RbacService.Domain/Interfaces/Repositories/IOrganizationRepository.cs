using RbacService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        Task<IEnumerable<Organization>> GetChildOrganizationsAsync(Guid parentId);
    }
}
