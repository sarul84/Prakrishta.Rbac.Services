using RbacService.Application.Users.Commands;
using RbacService.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Application.Users.CommandHandlers
{
    public class DeleteUserHandler(IUnitOfWork rbacRepository)
    {
        private readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task Handle(DeleteUser command, CancellationToken cancellationToken)
        {
            var user = await _rbacRepository.Users.GetByIdAsync(command.UserId, cancellationToken) ?? throw new KeyNotFoundException($"User with ID {command.UserId} not found.");
            _rbacRepository.Users.Delete(user);
            await _rbacRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
