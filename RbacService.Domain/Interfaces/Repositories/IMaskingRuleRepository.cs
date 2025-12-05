using RbacService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IMaskingRuleRepository : IGenericRepository<MaskingRule>
    {
        Task<IEnumerable<MaskingRule>> GetRulesByFieldAsync(Guid piiFieldId);
        Task<IEnumerable<MaskingRule>> GetRulesByApplicationAsync(Guid applicationId);
    }
}
