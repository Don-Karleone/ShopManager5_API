using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using FluentAssertions.Execution;
using ShopManager5.Data.Storage_providers;
using ShopManager5.Integration.Tests.FakeResources;
using ShopManager5.Api.Data;

namespace ShopManager5.Integration.Tests.StorageProviders
{
    public class InvoiceStorageProviderTests : IDisposable
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IInvoiceStorageProvider _storage;
        private readonly StandardData _standardData;

        public InvoiceStorageProviderTests()
        {
            _contextFactory = new TestDbContextFactory(nameof(InvoiceStorageProviderTests));
            _storage = new InvoiceStorageProvider(_contextFactory);
            _standardData = new StandardData();
        }

        public void Dispose()
        {
            using var context = _contextFactory.CreateDbContext();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetInvoice_InvoiceExists_GetInvoice()
        {
            using var context = _contextFactory.CreateDbContext();
            var invoice = _standardData.ModelInvoice;
            await context.Invoices.AddAsync(invoice);
            await context.SaveChangesAsync();

            var result = await _storage.GetInvoice(_standardData.ModelInvoice.Id);

            using (new AssertionScope())
            {
                result.Should().BeEquivalentTo(_standardData.ModelInvoice, options => options
                    .Excluding(x => x.Client.Invoices)
                    .Excluding(x => x.Employee.Invoices)
                    .Excluding(x => x.Products[0].Invoices)
                    .IgnoringCyclicReferences());
                result.Products.Count.Should().Be(1);
            }
        }

        [Fact]
        public async Task GetInvoice_InvoiceNotExists_GetNull()
        {
            var result = await _storage.GetInvoice(1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task AddInvoice_WithCorrectData_InvoiceAdded()
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Clients.AddAsync(_standardData.ModelClient);
            await context.Employees.AddAsync(_standardData.ModelEmployee);
            await context.Products.AddAsync(_standardData.ModelProduct);
            await context.SaveChangesAsync();

            var invoice = _standardData.ModelInvoice;

            var result = await _storage.AddInvoice(invoice);

            var addedInvoice = await context.Invoices
                .Include(i => i.Client)
                .Include(i => i.Employee)
                .Include(i => i.Products)
                .FirstOrDefaultAsync(p => p.Id == invoice.Id);

            using (new AssertionScope())
            {
                result.Should().Be(invoice.Id);
                addedInvoice.Should().BeEquivalentTo(invoice, options => options
                .Excluding(i => i.Products[0].Quantity)
                    .IgnoringCyclicReferences());
            }
        }

        [Fact]
        public async Task DeleteInvoice_WithExistingInvoice_InvoiceDeleted()
        {
            using var context = _contextFactory.CreateDbContext();
            var invoice = _standardData.ModelInvoice;
            await context.Invoices.AddAsync(invoice);
            await context.SaveChangesAsync();

            var result = await _storage.DeleteInvoice(invoice.Id);

            using (new AssertionScope())
            {
                result.Should().BeTrue();
                context.Invoices.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task EditInvoice_WithCorrectData_InvoiceEdited()
        {
            using var context = _contextFactory.CreateDbContext();

            var invoice = _standardData.ModelInvoice;
            var newInvoice = _standardData.ModelInvoice2;

            await context.Invoices.AddAsync(invoice);
            await context.SaveChangesAsync();

            var result = await _storage.EditInvoice(invoice.Id, newInvoice);

            using (new AssertionScope())
            {
                result.Should().BeTrue();

                (await context.Invoices.FirstOrDefaultAsync()).Should().BeEquivalentTo(newInvoice, options => options
                .Excluding(x => x.Client)
                .Excluding(x => x.Employee)
                .Excluding(x => x.Products)
                .IgnoringCyclicReferences());
            }
        }
    }
}
