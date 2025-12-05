using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class PiiAccessLogRepository(RbacDbContext context) : GenericRepository<PiiAccessLog>(context), IPiiAccessLogRepository
    {
        public async Task<IEnumerable<PiiAccessLog>> GetLogsByCorrelationIdAsync(string correlationId)
        {
            return await _context.PiiAccessLogs
                .Where(log => log.CorrelationId == correlationId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PiiAccessLog>> GetLogsByTargetUserAsync(Guid targetUserId)
        {
            return await _context.PiiAccessLogs
                .Where(log => log.TargetUserId == targetUserId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PiiAccessLog>> GetLogsByUserAsync(Guid userId)
        {
            return await _context.PiiAccessLogs
                .Where(log => log.UserId == userId)
                .ToListAsync();
        }
    }
}
