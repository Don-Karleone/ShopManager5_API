using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShopManager5.Api.Data;
using ShopManager5.Integration.Tests.FakeResources;
using System.Data.Common;

namespace ShopManager5.Integration.Tests.FakeResources
{
    public class TestDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        private DbContextOptions<ApplicationDbContext> _options;
        private readonly DbConnection _connection;
        private readonly StandardData standardData;

        public TestDbContextFactory(string databaseName = "InMemoryTest")
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options;

            _connection = RelationalOptionsExtension.Extract(_options).Connection;
            standardData = new StandardData();
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public ApplicationDbContext CreateDbContext()
        {
            var context = new ApplicationDbContext(_options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
