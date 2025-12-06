using RbacService.Application.Organization.Commands;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Organization.CommandHandlers
{
    public class DeleteOrganizationHandler(IUnitOfWork rbacRepository)
    {
        public readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task Handle(DeleteOrganization command, CancellationToken cancellationToken)
        {
            var organization = await _rbacRepository.Organizations.GetByIdAsync(command.OrganizationId, cancellationToken) ?? throw new KeyNotFoundException($"Organization with ID {command.OrganizationId} not found.");
            _rbacRepository.Organizations.Delete(organization);
            await _rbacRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
