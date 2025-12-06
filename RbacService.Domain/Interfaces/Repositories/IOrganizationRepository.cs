using RbacService.Domain.Entities;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        Task<bool> ExistsByNameAsync(string name, Guid? excludeId, CancellationToken token);
        Task<IEnumerable<Organization>> GetChildOrganizationsAsync(Guid parentId, CancellationToken token);
    }
}
