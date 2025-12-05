using RbacService.Application.Users.Commands;
using RbacService.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Application.Users.CommandHandlers
{
    public class CreateUserHandler(IUnitOfWork rbacRepository)
    {
        public readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task<Guid> Handle(CreateUser command, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.User
            {
                UserId = Guid.NewGuid(),
                Email = command.Email,
                Name = command.Name,
                Designation = command.Designation,
                DepartmentId = command.DepartmentId,
                OrganizationId = command.OrganizationId,
                ApplicationId = command.ApplicationId,
                ManagerId = command.ManagerId
            };

            await _rbacRepository.Users.AddAsync(user, cancellationToken);
            await _rbacRepository.SaveChangesAsync(cancellationToken);
            return user.UserId;
        }
    }
}
