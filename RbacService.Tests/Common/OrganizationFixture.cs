using RbacService.Domain.Entities;
using RbacService.Infrastructure.Repositories;

namespace RbacService.Tests.Common
{
    public class OrganizationFixture : RbacFixtureBase
    {
        public OrganizationFixture()
        {
            MockUnitOfWork.Setup(u => u.Organizations).Returns(new OrganizationRepository(DbContext));
        }

        public async Task<List<Organization>> SeedOrganizationsAsync(int count, Guid? parentOrgId = null)
        {
            var orgs = Enumerable.Range(0, count).Select(i => new Organization
            {
               Name = $"Organization {i}",
                Type = "Type A",
                Description = $"Description for Organization {i}",
                CreatedAt = DateTime.UtcNow,
                ParentOrganizationId = parentOrgId ?? Guid.NewGuid(),
            }).ToList();

            return await SeedEntitiesAsync<Organization>(orgs);
        }
    }
}
