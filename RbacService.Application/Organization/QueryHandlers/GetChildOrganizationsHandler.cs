using RbacService.Application.DTOs.Organization;
using RbacService.Application.Mappings;
using RbacService.Application.Organization.Queries;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Organization.QueryHandlers
{
    public class GetChildOrganizationsHandler(IUnitOfWork rbacRepository)
    {
        private readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task<IReadOnlyList<OrganizationDto>> Handle(GetChildOrganizations query, CancellationToken cancellationToken)
        {
            var organizations = await _rbacRepository.Organizations.GetChildOrganizationsAsync(query.parentOrganizationId, cancellationToken);
            return [.. organizations.Select(u => u.ToDto())];
        }
    }
}
