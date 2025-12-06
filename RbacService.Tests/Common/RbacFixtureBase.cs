using Moq;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Tests.Common
{
    public class RbacFixtureBase: IDisposable
    {
        public RbacDbContext DbContext { get; }
        public Mock<IUnitOfWork> MockUnitOfWork { get; }

        protected RbacFixtureBase()
        {
            // Fresh in-memory DbContext per test run
            DbContext = FakeDbContextFactory.CreateDbContext(Guid.NewGuid().ToString());

            // Wire UnitOfWork with DbContext-backed repositories
            MockUnitOfWork = new Mock<IUnitOfWork>();
            MockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                          .Returns(() => DbContext.SaveChangesAsync());
        }

        protected async Task<T> SeedEntityAsync<T>(T entity) where T : class
        {
            DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        protected async Task<List<T>> SeedEntitiesAsync<T>(IEnumerable<T> entities) where T : class
        {
            DbContext.Set<T>().AddRange(entities);
            await DbContext.SaveChangesAsync();
            return entities.ToList();
        }


        public void Dispose()
        {
            DbContext.Dispose();
            DbContext.Database.EnsureDeleted();
        }
    }
}
