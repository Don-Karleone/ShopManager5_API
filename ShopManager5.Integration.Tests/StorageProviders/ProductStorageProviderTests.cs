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
    public class ProductStorageProviderTests : IDisposable
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IProductStorageProvider _storage;
        private readonly StandardData _standardData;

        public ProductStorageProviderTests()
        {
            _contextFactory = new TestDbContextFactory(nameof(ProductStorageProviderTests));
            _storage = new ProductStorageProvider(_contextFactory);
            _standardData = new StandardData();
        }

        public void Dispose()
        {
            using var context = _contextFactory.CreateDbContext();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetProduct_ProductExists_GetProduct()
        {
            using var context = _contextFactory.CreateDbContext();

            context.Products.Add(_standardData.ModelProduct);
            await context.SaveChangesAsync();

            var result = await _storage.GetProduct(_standardData.ModelProduct.Id);

            result.Should().BeEquivalentTo(_standardData.ModelProduct, options => options
                .Excluding(p=>p.Invoices)
                .IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetProduct_ProductNotExists_GetNull()
        {
            var result = await _storage.GetProduct(1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task AddProduct_CorrectData_ProductAdded()
        {
            using var context = _contextFactory.CreateDbContext();

            var product = _standardData.ModelProduct;

            var result = await _storage.AddProduct(product);

            var addedProduct = await context.Products.SingleOrDefaultAsync(p => p.Id == product.Id);

            using (new AssertionScope())
            {
                result.Should().Be(product.Id);
                addedProduct.Should().BeEquivalentTo(product, options => options
                .IgnoringCyclicReferences());
            }
        }

        [Fact]
        public async Task DeleteProduct_WithExistingProduct_ProductDeleted()
        {
            using var context = _contextFactory.CreateDbContext();

            var product = _standardData.ModelProduct;

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var result = await _storage.DeleteProduct(product.Id);

            using (new AssertionScope())
            {
                result.Should().BeTrue();
                context.Products.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task EditProduct_WithCorrectData_ProductEdited()
        {
            using var context = _contextFactory.CreateDbContext();

            var product = _standardData.ModelProduct;
            var newProduct = _standardData.ModelProduct2;

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var result = await _storage.EditProduct(product.Id, newProduct);

            using (new AssertionScope())
            {
                using var context2 = _contextFactory.CreateDbContext();
                result.Should().BeTrue();

                (await context2.Products.FirstOrDefaultAsync()).Should().BeEquivalentTo(newProduct, options => options
                .Excluding(x => x.Category)
                .IgnoringCyclicReferences());
            }
        }
    }
}
