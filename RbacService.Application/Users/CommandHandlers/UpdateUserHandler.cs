using RbacService.Application.Users.Commands;
using RbacService.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Application.Users.CommandHandlers
{
    public class UpdateUserHandler(IUnitOfWork rbacRepository)
    {
        private readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task Handle(UpdateUser command, CancellationToken cancellationToken)
        {
            var user = await _rbacRepository.Users.GetByIdAsync(command.UserId, cancellationToken) ?? throw new KeyNotFoundException($"User with ID {command.UserId} not found.");
            user.Email = command.Email;
            user.Name = command.Name;
            user.Designation = command.Designation;
            user.DepartmentId = command.DepartmentId;
            user.OrganizationId = command.OrganizationId;
            user.ApplicationId = command.ApplicationId;
            user.ManagerId = command.ManagerId;
            user.LastLoginAt = command.LastLoginAt;

            _rbacRepository.Users.Update(user);
            await _rbacRepository.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
