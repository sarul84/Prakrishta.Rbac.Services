using FluentAssertions;
using RbacService.Application.Users.Queries;
using RbacService.Application.Users.QueryHandlers;
using RbacService.Tests.Common;

namespace RbacService.Tests.Queires.User
{
    public class GetUserByIdHandlerTests(UserFixture fixture) : IClassFixture<UserFixture>
    {
        private readonly UserFixture _fixture = fixture;

        [Fact]
        public async Task Handle_ShouldReturnUser_WhenIdExists()
        {
            // Arrange
            var seededUser = await _fixture.SeedUsersAsync(10);
            var handler = new GetUserByIdHandler(_fixture.MockUnitOfWork.Object);

            //Act
            var result = handler.Handle(new GetUserById(seededUser[8].UserId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Result.UserId.Should().Be(seededUser[8].UserId);
            result.Result.Email.Should().Be("user8@example.com");
        }
    }
}
