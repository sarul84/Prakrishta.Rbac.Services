using FluentValidation;
using ImTools;
using System.Linq.Expressions;

namespace RbacService.Application.Validators.User
{
    public abstract class UserValidatorBase<TCommand> : AbstractValidator<TCommand>
    {
        protected void AddCommonRules(
            Expression<Func<TCommand, string>> emailSelector,
            Expression<Func<TCommand, string>> nameSelector,
            Expression<Func<TCommand, string?>> designationSelector)
        {
            RuleFor(emailSelector)
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(250).WithMessage("Email must not exceed 250 characters.")
                .EmailAddress().WithMessage("Email must be a valid email address");

            RuleFor(nameSelector)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(nameSelector)
                .Matches(@"^[a-zA-Z0-9 .]+$")
                .WithMessage("Name can only contain letters, numbers, spaces, and periods.")
                .When(x => !string.IsNullOrWhiteSpace(nameSelector.Compile().Invoke(x)));

            RuleFor(designationSelector)
                .MaximumLength(50).WithMessage("Designation must not exceed 50 characters")
                .When(x => !string.IsNullOrWhiteSpace(designationSelector.Compile().Invoke(x)));
        }
    }
}
