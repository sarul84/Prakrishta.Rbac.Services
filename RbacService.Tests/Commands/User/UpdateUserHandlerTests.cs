using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RbacService.Application.Users.CommandHandlers;
using RbacService.Application.Users.Commands;
using RbacService.Tests.Common;

namespace RbacService.Tests.Commands.User
{
    public class UpdateUserHandlerTests(UserFixture fixture) : IClassFixture<UserFixture>
    {
        private readonly UserFixture _fixture = fixture;

        [Fact]
        public async Task Should_Throw_WhenUserNotFound()
        {
            var handler = new UpdateUserHandler(_fixture.MockUnitOfWork.Object);
            var command = new UpdateUser(Guid.NewGuid(), "New Name", "new@example.com",  null,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null);

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"User with ID {command.UserId} not found.");
        }

        [Fact]
        public async Task Should_UpdateUser_WhenValid()
        {
            // Arrange: get seeded user
            var handler = new UpdateUserHandler(_fixture.MockUnitOfWork.Object);
            await _fixture.SeedUsersAsync(3);
            var existingUser = await _fixture.DbContext.Users.FirstAsync();
            var command = new UpdateUser(existingUser.UserId, "New Name", "new@example.com",  "New Designation",
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var updatedUser = await _fixture.DbContext.Users.FindAsync(existingUser.UserId);
            updatedUser.Email.Should().Be("new@example.com");
            updatedUser.Name.Should().Be("New Name");
            updatedUser.Designation.Should().Be("New Designation");
        }

        [Fact]
        public async Task Should_UpdateLastLoginAt_WhenProvided()
        {
            var handler = new UpdateUserHandler(_fixture.MockUnitOfWork.Object);
            await _fixture.SeedUsersAsync(3);
            var existingUser = await _fixture.DbContext.Users.FirstAsync();
            var newLogin = DateTime.UtcNow;

            var command = new UpdateUser(existingUser.UserId, existingUser.Name, existingUser.Email, 
                existingUser.Designation,  existingUser.OrganizationId,
                existingUser.ApplicationId, existingUser.DepartmentId, existingUser.ManagerId, newLogin);

            await handler.Handle(command, CancellationToken.None);

            var updatedUser = await _fixture.DbContext.Users.FindAsync(existingUser.UserId);
            updatedUser.LastLoginAt.Should().Be(newLogin);
        }


    }
}
