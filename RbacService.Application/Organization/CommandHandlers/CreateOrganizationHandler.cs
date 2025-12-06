using RbacService.Application.Organization.Commands;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Organization.CommandHandlers
{
    public class CreateOrganizationHandler(IUnitOfWork rbacRepository)
    {
        public readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task<Guid> Handle(CreateOrganization command, CancellationToken cancellationToken)
        {
            var organization = new Domain.Entities.Organization
            {
                OrganizationId = Guid.NewGuid(),
                Name = command.Name,
                Description = command.Description,
                ParentOrganizationId = command.ParentOrganizationId,
                Type = command.Type
            };

            await _rbacRepository.Organizations.AddAsync(organization, cancellationToken);
            await _rbacRepository.SaveChangesAsync(cancellationToken);
            return organization.OrganizationId;
        }
    }
}
