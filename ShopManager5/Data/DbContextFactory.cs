using Microsoft.EntityFrameworkCore;

namespace ShopManager5.Api.Data
{
    public class DbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DevConnectionString");

            builder.UseSqlServer(connectionString);

            var context = new ApplicationDbContext(builder.Options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
