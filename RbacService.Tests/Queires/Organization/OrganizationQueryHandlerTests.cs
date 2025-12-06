using FluentAssertions;
using RbacService.Application.Organization.Queries;
using RbacService.Application.Organization.QueryHandlers;
using RbacService.Tests.Common;

namespace RbacService.Tests.Queires.Organization
{
    public class OrganizationQueryHandlerTests(OrganizationFixture organizationFixture) : IClassFixture<OrganizationFixture>
    {
        private readonly OrganizationFixture _organizationFixture = organizationFixture;

        [Fact]
        public async Task GetAllOrganizationsHandler_ShouldReturnAllOrganizations()
        {
            // Arrange
            await _organizationFixture.SeedOrganizationsAsync(10);
            var handler = new GetAllOrganizationHandler(_organizationFixture.MockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(new GetAllOrganizations(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(10);

            await _organizationFixture.DbContext.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task GetChildOrganizationsHandler_ShouldReturnChildOrganizations()
        {
            // Arrange
            var parentOrgId = Guid.NewGuid();
            var orgs = await _organizationFixture.SeedOrganizationsAsync(5, parentOrgId);

            var handler = new GetChildOrganizationsHandler(_organizationFixture.MockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(new GetChildOrganizations(parentOrgId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result[0].ParentOrganizationId.Should().Be(parentOrgId);
            result.Should().HaveCount(5);

            await _organizationFixture.DbContext.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_Return_Organization_WhenIdPresent()
        {
            // Arrange
            var orgs = await _organizationFixture.SeedOrganizationsAsync(5);            
            var handler = new GetOrganizationByIdHandler(_organizationFixture.MockUnitOfWork.Object);
            var org = orgs[0];

            // Act
            var result = await handler.Handle(new GetOrganizationById(org.OrganizationId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.OrganizationId.Should().Be(org.OrganizationId);

            await _organizationFixture.DbContext.Database.EnsureDeletedAsync();
        }
    }
}
