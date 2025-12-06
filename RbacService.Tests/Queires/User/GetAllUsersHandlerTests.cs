using FluentAssertions;
using RbacService.Application.Users.Queries;
using RbacService.Application.Users.QueryHandlers;
using RbacService.Tests.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Tests.Queires.User
{
    public class GetAllUsersHandlerTests(UserFixture fixture) : IClassFixture<UserFixture>
    {
        private readonly UserFixture _fixture = fixture;

        [Fact]
        public async Task Handle_ShouldReturnAllUsers_WhenCalled()
        {
            // Arrange
            await _fixture.SeedUsersAsync(10); // Ensure there are users in the test database
            var handler = new GetAllUsersHandler(_fixture.MockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(new GetAllUsers(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterThanOrEqualTo(10); // Assuming there are users in the test database
        }
    }
}
