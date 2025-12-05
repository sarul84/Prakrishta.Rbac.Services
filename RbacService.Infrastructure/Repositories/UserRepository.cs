using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class UserRepository(RbacDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<bool> ExistsByEmailAsync(string email, Guid? excludeUserId, CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.UserId != excludeUserId, cancellationToken);
            return result != null;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        public async Task<IEnumerable<User>> GetUsersByOrganizationAsync(Guid organizationId, CancellationToken cancellationToken)
            => await _context.Users.Where(u => u.OrganizationId == organizationId).ToListAsync(cancellationToken);
    }
}
