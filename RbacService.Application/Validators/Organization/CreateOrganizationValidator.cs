using FluentValidation;
using RbacService.Application.Organization.Commands;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Validators.Organization
{
    public class CreateOrganizationValidator : OrganizationValidatorBase<CreateOrganization>
    {
        public CreateOrganizationValidator(IOrganizationRepository repository)
        {
            AddCommonRules(x => x.Name, x => x.Description);

            RuleFor(x => x.Name)
                .MustAsync(async (name, ct) => !await repository.ExistsByNameAsync(name, null, CancellationToken.None))
                .WithMessage("Organization name already exists");
        }
    }
}
