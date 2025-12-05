using Microsoft.EntityFrameworkCore;
using RbacService.Infrastructure.Data;

namespace RbacService.Tests.Common
{
    public static class FakeDbContextFactory
    {
        public static RbacDbContext CreateDbContext(string dbName = "RbacTestDb")
        {
            var options = new DbContextOptionsBuilder<RbacDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new RbacDbContext(options);
        }
    }

}
