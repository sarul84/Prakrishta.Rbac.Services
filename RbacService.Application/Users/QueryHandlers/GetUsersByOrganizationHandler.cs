using RbacService.Application.DTOs.User;
using RbacService.Application.Mappings;
using RbacService.Application.Users.Queries;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Users.QueryHandlers
{
    public class GetUsersByOrganizationHandler(IUnitOfWork rbacRepository)
    {
        private readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task<IReadOnlyList<UserDto>> Handle(GetUsersByOrganization query, CancellationToken cancellationToken)
        {
            var users = await _rbacRepository.Users.GetAllAsync(cancellationToken);
            return [.. users
                .Where(u => u.OrganizationId == query.OrganizationId)
                .Select(u => u.ToDto())];
        }
    }
}
