using RbacService.Domain.Entities;
using RbacService.Infrastructure.Repositories;

namespace RbacService.Tests.Common
{
    public class UserFixture : RbacFixtureBase
    {
        public UserFixture()
        {
            MockUnitOfWork.Setup(u => u.Users).Returns(new UserRepository(DbContext));
        }

        //Seeding Helpers
        public async Task<User> SeedUserAsync(string email, string name, string? designation = null)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = email,
                Name = name,
                Designation = designation,
                DepartmentId = Guid.NewGuid(),
                OrganizationId = Guid.NewGuid(),
                ApplicationId = Guid.NewGuid()
            };
            
            return await SeedEntityAsync<User>(user);
        }

        public async Task<List<User>> SeedUsersAsync(int count, Guid? orgId = null)
        {
            var users = Enumerable.Range(0, count).Select(i => new User
            {
                UserId = Guid.NewGuid(),
                Email = $"user{i}@example.com",
                Name = $"User {i}",
                Designation = "Dev",
                DepartmentId = Guid.NewGuid(),
                OrganizationId = orgId ?? Guid.NewGuid(),
                ApplicationId = Guid.NewGuid()
            }).ToList();
            
            return await SeedEntitiesAsync<User>(users);
        }
    }
}
