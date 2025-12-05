using RbacService.Domain.Entities;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ExistsByEmailAsync(string email, Guid? excludeUserId, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetUsersByOrganizationAsync(Guid organizationId, CancellationToken cancellationToken = default);
    }
}
