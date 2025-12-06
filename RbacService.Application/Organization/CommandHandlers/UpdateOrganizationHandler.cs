using RbacService.Application.Organization.Commands;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Organization.CommandHandlers
{
    public class UpdateOrganizationHandler(IUnitOfWork rbacRepository)
    {
        public readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task Handle(UpdateOrganization command, CancellationToken cancellationToken)
        {
            var organization = await _rbacRepository.Organizations.GetByIdAsync(command.OrganizationId, cancellationToken) ?? throw new KeyNotFoundException($"Organization with ID {command.OrganizationId} not found.");
            organization.Name = command.Name;
            organization.Description = command.Description;
            organization.ParentOrganizationId = command.ParentOrganizationId;
            organization.Type = command.Type;

            _rbacRepository.Organizations.Update(organization);
            await _rbacRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
