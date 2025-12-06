using FluentValidation;
using System.Linq.Expressions;

namespace RbacService.Application.Validators.Organization
{
    public abstract class OrganizationValidatorBase<TCommand> : AbstractValidator<TCommand>
    {
        protected void AddCommonRules(
            Expression<Func<TCommand, string>> organizationNameSelector,
            Expression<Func<TCommand, string>> organizationDescriptionSelector)
        {
            RuleFor(organizationNameSelector)
                .NotEmpty().WithMessage("Organization Name is required")
                .MaximumLength(50).WithMessage("Organization Name must not exceed 50 characters.")
                .Matches(@"^[a-zA-Z0-9\s,&'.]+$")
                    .WithMessage("Organization Name may only contain letters, numbers, spaces, commas, ampersands, apostrophes, and periods.");

            RuleFor(organizationDescriptionSelector)
                .MaximumLength(250).WithMessage("Organization Description must not exceed 250 characters.")
                .Matches(@"^[a-zA-Z0-9\s,&'.]+$")
                    .WithMessage("Description may only contain letters, numbers, spaces, commas, ampersands, apostrophes, and periods.")
                .When(x => !string.IsNullOrWhiteSpace(organizationDescriptionSelector.Compile().Invoke(x)));
        }
    }
}
