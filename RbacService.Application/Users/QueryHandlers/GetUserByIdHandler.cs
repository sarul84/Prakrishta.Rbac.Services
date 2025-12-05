using RbacService.Application.DTOs;
using RbacService.Application.Mappings;
using RbacService.Application.Users.Queries;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Users.QueryHandlers
{
    public class GetUserByIdHandler(IUnitOfWork rbacRepository)
    {
        private readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task<UserDto?> Handle(GetUserById query, CancellationToken cancellationToken)
        {
            var user = await _rbacRepository.Users.GetByIdAsync(query.UserId, cancellationToken);
            if (user is null) return null;

            return user.ToDto();
        }
    }
}
