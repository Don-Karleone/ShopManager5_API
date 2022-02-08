using Microsoft.EntityFrameworkCore;
using ShopManager5.Api.Data;
using ShopManager5.Api.Data.Models;

namespace ShopManager5.Data.Storage_providers
{
    public class ClientStorageProvider : IClientStorageProvider
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ClientStorageProvider(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Client>> GetClients()
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Clients
                .Include(c => c.Invoices)
                .ToListAsync();
        }

        public async Task<Client> GetClient(int id)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Clients
                .Include(c => c.Invoices)
                .FirstOrDefaultAsync(client => client.Id == id);
        }

        public async Task<int> AddClient(Client client)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            var result = await dbContext.Clients.AddAsync(client);
            await dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<bool> EditClient(int id, Client newClient)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            var dbClient = await dbContext.Clients
                .Include(c => c.Invoices)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (dbClient is null) return false;

            newClient.Id = dbClient.Id;

            dbContext.CopyEntityMembers(newClient, dbClient);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteClient(int id)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            var dbClient = await dbContext.Clients
                .Include(c => c.Invoices)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (dbClient is null) return false;

            dbContext.Remove(dbClient);

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
