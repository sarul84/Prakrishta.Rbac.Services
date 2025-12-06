using FluentAssertions;
using MassTransit;
using RbacService.Application.Users.Queries;
using RbacService.Application.Users.QueryHandlers;
using RbacService.Domain.Entities;
using RbacService.Tests.Common;
using System.Reflection.Metadata;

namespace RbacService.Tests.Queires.User
{
    public class GetUsersByOrganizationHandlerTests(UserFixture fixture) : IClassFixture<UserFixture>
    {
        private readonly UserFixture _fixture = fixture;

        [Fact]
        public async Task Handle_ShouldReturnUsers_WhenOrganizationExists()
        {
            // Arrange
            var orgId = Guid.NewGuid();
            await _fixture.SeedUsersAsync(10, orgId: orgId); // Ensure there are users in the test database with the specified organization
            var handler = new GetUsersByOrganizationHandler(_fixture.MockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(new GetUsersByOrganization(orgId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterThanOrEqualTo(10); // Assuming there are users in the test database for the organization
        }

        [Fact]
        public async Task Handle_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var orgId = Guid.NewGuid();
            await _fixture.SeedUsersAsync(10, orgId: orgId);
            var handler = new GetUsersByOrganizationHandler(_fixture.MockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(new GetUsersByOrganization(Guid.NewGuid()), CancellationToken.None);

            Assert.Empty(result);
        }

    }
}
