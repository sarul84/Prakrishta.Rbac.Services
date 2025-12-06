using Moq;
using RbacService.Application.Users.CommandHandlers;
using RbacService.Tests.Common;

namespace RbacService.Tests.Commands.User
{
    public class DeleteUserHandlerTests(UserFixture fixture) : IClassFixture<UserFixture>
    {
        private readonly UserFixture _fixture = fixture;

        [Fact]
        public async Task DeleteUserHandler_Should_Delete_User()
        {
            // Arrange
            var existingUsers = await _fixture.SeedUsersAsync(5);

            var handler = new DeleteUserHandler(_fixture.MockUnitOfWork.Object);
            var userId = existingUsers[2].UserId;
            var command = new RbacService.Application.Users.Commands.DeleteUser(userId);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _fixture.MockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }
    }
}
