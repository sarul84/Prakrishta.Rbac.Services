using FluentValidation;
using RbacService.Application.Organization.Commands;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Validators.Organization
{
    public class UpdateOrganizationValidator : OrganizationValidatorBase<UpdateOrganization>
    {
        public UpdateOrganizationValidator(IOrganizationRepository repository)
        {
            AddCommonRules(x => x.Name, x => x.Description);

            RuleFor(x => x.OrganizationId)
                .MustAsync(async (id, ct) => await repository.GetByIdAsync(id) != null)
                .WithMessage("Organization does not exist");

            RuleFor(x => x.Name)
                .MustAsync(async (cmd, name, ct) =>
                    !await repository.ExistsByNameAsync(name, excludeId: cmd.OrganizationId, ct))
                .WithMessage("Organization name already exists");
        }
    }
}
