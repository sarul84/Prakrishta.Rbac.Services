using RbacService.Application.DTOs;
using RbacService.Application.Mappings;
using RbacService.Application.Users.Queries;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Users.QueryHandlers
{
    public class GetAllUsersHandler(IUnitOfWork rbacRepository)
    {
        private readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task<IReadOnlyList<UserDto>> Handle(GetAllUsers query, CancellationToken cancellationToken)
        {
            var users = await _rbacRepository.Users.GetAllAsync(cancellationToken);
            return [.. users.Select(u => u.ToDto())];
        }
    }
}
