using Microsoft.EntityFrameworkCore;
using ShopManager5.Api.Data;
using ShopManager5.Api.Data.Models;

namespace ShopManager5.Data.Storage_providers
{
    public class ProductStorageProvider : IProductStorageProvider
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ProductStorageProvider(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<int> AddProduct(Product product)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            var resultEntry = await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return resultEntry.Entity.Id;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            var product = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return false;

            dbContext.Remove(product);
            dbContext.SaveChanges();
            
            return true;
        }

        public async Task<bool> EditProduct(int id, Product product)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            var dbProduct = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (dbProduct is null) return false;

            product.Id = dbProduct.Id;

            dbContext.CopyEntityMembers(product, dbProduct);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Product> GetProduct(int id)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

#pragma warning disable CS8603 // Możliwe zwrócenie odwołania o wartości null.
            return await dbContext.Products
                .Include(p=>p.Invoices)
                .FirstOrDefaultAsync(p => p.Id == id);
#pragma warning restore CS8603 // Możliwe zwrócenie odwołania o wartości null.
        }

        public async Task<List<Product>> GetProducts()
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Products
                .Include(p => p.Invoices)
                .ToListAsync();
        }
    }
}
