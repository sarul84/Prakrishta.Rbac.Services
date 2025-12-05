using FluentValidation;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Validators.User
{
    public class UpdateUserValidator: UserValidatorBase<Users.Commands.UpdateUser>
    {
        public UpdateUserValidator(IUserRepository users)
        {
            AddCommonRules(x => x.Email, x => x.Name, x => x.Designation);

            RuleFor(x => x.UserId)
                .MustAsync(async (id, ct) => await users.GetByIdAsync(id) != null)
                .WithMessage("User does not exist");

            RuleFor(x => x.Email)
                .MustAsync(async (cmd, email, ct) =>
                    !await users.ExistsByEmailAsync(email, excludeUserId: cmd.UserId))
                .WithMessage("Email already exists");
        }

    }
}
