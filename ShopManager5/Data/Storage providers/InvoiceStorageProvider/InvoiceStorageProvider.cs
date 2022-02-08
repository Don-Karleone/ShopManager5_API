using Microsoft.EntityFrameworkCore;
using ShopManager5.Api.Data;
using ShopManager5.Api.Data.Models;

namespace ShopManager5.Data.Storage_providers
{
    public class InvoiceStorageProvider : IInvoiceStorageProvider
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;


        public InvoiceStorageProvider(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<int> AddInvoice(Invoice invoice)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            var client = await dbContext.Clients
                .Include(c => c.Invoices)
                .FirstOrDefaultAsync(c => c.Id == invoice.Client.Id);
            if (client != null) 
                invoice.Client = client;

            var employee = await dbContext.Employees
                .Include(e => e.Invoices)
                .FirstOrDefaultAsync(e => e.Id == invoice.Employee.Id);
            if (employee != null)
                invoice.Employee = employee;

            for (int i = 0; i<invoice.Products.Count(); i++)
            {
                var product = await dbContext.Products
                    .Include(c => c.Invoices)
                    .FirstOrDefaultAsync(p => p.Id == invoice.Products[i].Id);
                if (product != null)
                    product.Quantity -= invoice.Products[i].Quantity;
                    invoice.Products[i] = product;
            }

            var resultEntry = await dbContext.Invoices.AddAsync(invoice);
            await dbContext.SaveChangesAsync();

            return resultEntry.Entity.Id;
        }

        public async Task<bool> DeleteInvoice(int id)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            var dbInvoice = await dbContext.Invoices
                .Include(i => i.Client)
                .Include(i => i.Employee)
                .Include(i=> i.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (dbInvoice is null) return false;

            dbContext.Invoices.Remove(dbInvoice);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditInvoice(int id, Invoice invoice)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            var dbInvoice = await dbContext.Invoices
                .Include(i => i.Client)
                .Include(i => i.Employee)
                .Include(i => i.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (dbInvoice is null) return false;

            invoice.Id = dbInvoice.Id;

            dbContext.CopyEntityMembers(dbInvoice, invoice);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Invoice>> GetInvoices()
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Invoices
                .Include(c => c.Client)
                .Include(c => c.Employee)
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<Invoice> GetInvoice(int id)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Invoices
                .Include(c => c.Client)
                .Include(c => c.Employee)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
