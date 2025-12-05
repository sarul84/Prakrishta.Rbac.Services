using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class MaskingRuleRepository(RbacDbContext context) : GenericRepository<MaskingRule>(context), IMaskingRuleRepository
    {
        public async Task<IEnumerable<MaskingRule>> GetRulesByApplicationAsync(Guid applicationId)
        {
            return await _context.MaskingRules
                .Where(mr => mr.ApplicationId == applicationId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MaskingRule>> GetRulesByFieldAsync(Guid piiFieldId)
        {
            return await _context.MaskingRules
                .Where(mr => mr.PiiFieldId == piiFieldId)
                .ToListAsync();
        }
    }
}
