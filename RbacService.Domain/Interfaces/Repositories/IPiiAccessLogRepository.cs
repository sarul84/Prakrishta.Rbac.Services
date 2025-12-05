using RbacService.Domain.Entities;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IPiiAccessLogRepository : IGenericRepository<PiiAccessLog>
    {
        Task<IEnumerable<PiiAccessLog>> GetLogsByUserAsync(Guid userId);
        Task<IEnumerable<PiiAccessLog>> GetLogsByTargetUserAsync(Guid targetUserId);
        Task<IEnumerable<PiiAccessLog>> GetLogsByCorrelationIdAsync(string correlationId);
    }
}
