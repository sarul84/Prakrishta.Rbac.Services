using FluentAssertions;
using RbacService.Application.Users.Queries;
using RbacService.Application.Users.QueryHandlers;
using RbacService.Tests.Common;

namespace RbacService.Tests.Queires.User
{
    public class GetUserByEmailHandlerTests(UserFixture fixture) : IClassFixture<UserFixture>
    {
        private readonly UserFixture _fixture = fixture;

        [Fact]
        public async Task Handle_ShouldReturnUser_WhenEmailExists()
        {
            // Arrange
            var seededUser = await _fixture.SeedUsersAsync(10);
            var handler = new GetUserByEmailHandler(_fixture.MockUnitOfWork.Object);

            //Act
            var result = handler.Handle(new GetUserByEmail("user8@example.com"), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Result.Email.Should().Be("user8@example.com");
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenEmailDoesNotExist()
        {
            // Arrange
            await _fixture.SeedUsersAsync(10);
            var handler = new GetUserByEmailHandler(_fixture.MockUnitOfWork.Object);

            //Act
            var result = handler.Handle(new GetUserByEmail("random@example.com"), CancellationToken.None);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldBeCaseInsensitive_WhenEmailExists()
        {
            // Arrange
            var seededUser = await _fixture.SeedUsersAsync(10);
            var handler = new GetUserByEmailHandler(_fixture.MockUnitOfWork.Object);

            //Act
            var result = handler.Handle(new GetUserByEmail("USER9@example.com"), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Result.Email.Should().Be("user9@example.com");
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenEmailIsNull()
        {
            // Arrange
            await _fixture.SeedUsersAsync(10);
            var handler = new GetUserByEmailHandler(_fixture.MockUnitOfWork.Object);

            //Act
            var result = handler.Handle(new GetUserByEmail(null!), CancellationToken.None);

            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenEmailIsEmpty()
        {
            // Arrange
            await _fixture.SeedUsersAsync(10);
            var handler = new GetUserByEmailHandler(_fixture.MockUnitOfWork.Object);
            
            //Act
            var result = handler.Handle(new GetUserByEmail(string.Empty), CancellationToken.None);
            
            // Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenEmailIsWhitespace()
        {
            // Arrange
            await _fixture.SeedUsersAsync(10);
            var handler = new GetUserByEmailHandler(_fixture.MockUnitOfWork.Object);
            
            //Act
            var result = handler.Handle(new GetUserByEmail("   "), CancellationToken.None);
            
            // Assert
            result.Result.Should().BeNull();
        }        
    }
}
