using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Organization.QueryHandlers
{
    public class GetOrganizationByIdHandler(IUnitOfWork rbacRepository)
    {
        public readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task<Domain.Entities.Organization> Handle(Queries.GetOrganizationById query, CancellationToken cancellationToken)
        {
            var organization = await _rbacRepository.Organizations.GetByIdAsync(query.OrganizationId, cancellationToken);
            return organization;
        }
    }
}
