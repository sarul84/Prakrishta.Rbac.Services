using FluentValidation;
using RbacService.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
