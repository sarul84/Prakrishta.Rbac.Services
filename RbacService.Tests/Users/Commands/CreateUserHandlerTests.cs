using FluentAssertions;
using RbacService.Application.Exceptions;
using RbacService.Application.Users.CommandHandlers;
using RbacService.Application.Users.Commands;
using RbacService.Tests.Common;

namespace RbacService.Tests.Users.Commands
{
    public class CreateUserHandlerTests(UserFixture fixture) : IClassFixture<UserFixture>
    {
        private readonly UserFixture _fixture = fixture;

        [Fact]
        public async Task Handle_ShouldCreateUser_WhenCommandIsValid()
        {
            // Arrange
            var handler = new CreateUserHandler(_fixture.MockUnitOfWork.Object);
            var command = new CreateUser("Valid User", "valid@example.com", "Dev",
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var user = await _fixture.DbContext.Users.FindAsync(result);
            user.Should().NotBeNull();
            user!.Email.Should().Be("valid@example.com");
        }        

        [Fact]
        public async Task Handle_ShouldPersistUserWithCorrectValues()
        {
            // Arrange
            var handler = new CreateUserHandler(_fixture.MockUnitOfWork.Object);
            var command = new CreateUser("Persisted User", "persist@example.com", "Dev",
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var user = _fixture.DbContext.Users.FirstOrDefault(u => u.UserId == result);
            user.Should().NotBeNull();
            user!.Email.Should().Be("persist@example.com");
            user.Name.Should().Be("Persisted User");
            user.Designation.Should().Be("Dev");
        }

    }
}
