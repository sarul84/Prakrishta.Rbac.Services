using Moq;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;
using RbacService.Infrastructure.Repositories;

namespace RbacService.Tests.Common
{
    public class UserFixture : IDisposable
    {
        public RbacDbContext DbContext { get; }
        public Mock<IUnitOfWork> MockUnitOfWork { get; }

        public UserFixture()
        {
            // Create a fresh in-memory DbContext for each test run
            DbContext = FakeDbContextFactory.CreateDbContext(Guid.NewGuid().ToString());

            // Wire UnitOfWork to use real UserRepository backed by DbContext
            MockUnitOfWork = new Mock<IUnitOfWork>();
            MockUnitOfWork.Setup(u => u.Users).Returns(new UserRepository(DbContext));
            MockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                          .Returns(() => DbContext.SaveChangesAsync());
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

            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> SeedUsersAsync(int count)
        {
            var users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                users.Add(new User
                {
                    UserId = Guid.NewGuid(),
                    Email = $"user{i}@example.com",
                    Name = $"User {i}",
                    Designation = "Dev",
                    DepartmentId = Guid.NewGuid(),
                    OrganizationId = Guid.NewGuid(),
                    ApplicationId = Guid.NewGuid()
                });
            }

            DbContext.Users.AddRange(users);
            await DbContext.SaveChangesAsync();
            return users;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

    }
}
