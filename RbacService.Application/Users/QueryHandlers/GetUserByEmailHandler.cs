using RbacService.Application.DTOs;
using RbacService.Application.Mappings;
using RbacService.Application.Users.Queries;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Application.Users.QueryHandlers
{
    public class GetUserByEmailHandler(IUnitOfWork rbacRepository)
    {
        private readonly IUnitOfWork _rbacRepository = rbacRepository;

        public async Task<UserDto?> Handle(GetUserByEmail query, CancellationToken cancellationToken)
        {
            var users = await _rbacRepository.Users.GetAllAsync(cancellationToken);
            var user = users.FirstOrDefault(u => u.Email == query.Email);
            if (user is null) return null;

            return user.ToDto();
        }
    }
}
