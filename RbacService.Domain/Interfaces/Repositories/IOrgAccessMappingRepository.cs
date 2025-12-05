using RbacService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IOrgAccessMappingRepository : IGenericRepository<OrgAccessMapping>
    {
        Task<IEnumerable<OrgAccessMapping>> GetMappingsBySourceOrgAsync(Guid sourceOrgId);
    }
}
