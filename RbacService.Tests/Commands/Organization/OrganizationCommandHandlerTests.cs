using FluentAssertions;
using Moq;
using RbacService.Application.Organization.CommandHandlers;
using RbacService.Application.Organization.Commands;
using RbacService.Tests.Common;

namespace RbacService.Tests.Commands.Organization
{
    public class OrganizationCommandHandlerTests(OrganizationFixture organizationFixture) : IClassFixture<OrganizationFixture>
    {
        private readonly OrganizationFixture _organizationFixture = organizationFixture;

        [Fact]
        public async Task CreateOrganization_ShouldCreateOrganization()
        {
            // Arrange
            var handler = new CreateOrganizationHandler(_organizationFixture.MockUnitOfWork.Object);
            var command = new CreateOrganization(
                "New Organization",
                "Description of new organization",
                "Department",
                Guid.NewGuid()                
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task Should_UpdateOrganization_WhenValid()
        {
            // Arrange
            var orgs = await _organizationFixture.SeedOrganizationsAsync(5);
            var handler = new UpdateOrganizationHandler(_organizationFixture.MockUnitOfWork.Object);
            var existingOrgId = orgs[2].OrganizationId;
            var command = new UpdateOrganization(
                existingOrgId,
                "Updated Organization",
                "Updated description",
                "Division",
                null
            );

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _organizationFixture.MockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task Should_DeleteOrganization_WhenValid()
        {
            // Arrange
            var orgs = await _organizationFixture.SeedOrganizationsAsync(5);
            var handler = new DeleteOrganizationHandler(_organizationFixture.MockUnitOfWork.Object);
            var existingOrgId = orgs[3].OrganizationId;
            var command = new DeleteOrganization(existingOrgId);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _organizationFixture.MockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_WhenOrganizationNotFoundOnUpdate()
        {
            // Arrange
            var handler = new UpdateOrganizationHandler(_organizationFixture.MockUnitOfWork.Object);
            Guid existingOrgId = Guid.NewGuid();
            var command = new UpdateOrganization(
                existingOrgId,
                "Non-existent Organization",
                "This organization does not exist",
                "Team",
                null
            );

            // Act
            Func<Task> act = () => handler.Handle(command, CancellationToken.None);

            //Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Organization with ID {existingOrgId} not found.");
        }
    }
}
