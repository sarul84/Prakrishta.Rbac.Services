using FluentValidation;

namespace RbacService.Application.Validators.User
{
    public class GetUserByEmailValidator : AbstractValidator<Users.Queries.GetUserByEmail>
    {
        public GetUserByEmailValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(250).WithMessage("Email must not exceed 250 characters.")
                .EmailAddress().WithMessage("Email must be a valid email address");
        }
    }
}
