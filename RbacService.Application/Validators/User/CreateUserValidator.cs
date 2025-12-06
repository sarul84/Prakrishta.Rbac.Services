using FluentValidation;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Validators.User
{
    public class CreateUserValidator : UserValidatorBase<Users.Commands.CreateUser>
    {
        public CreateUserValidator(IUserRepository users)
        {
            AddCommonRules(x => x.Email, x => x.Name, x => x.Designation);

            RuleFor(x => x.Email)
                .MustAsync(async (email, ct) => !await users.ExistsByEmailAsync(email, null, CancellationToken.None))
                .WithMessage("Email already exists");
        }

    }
}
