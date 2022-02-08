using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using FluentAssertions;
using ShopManager5.Data.Storage_providers;
using ShopManager5.Integration.Tests.FakeResources;
using System.Threading.Tasks;
using FluentAssertions.Execution;
using ShopManager5.Api.Data;

namespace ShopManager5.Integration.Tests.StorageProviders
{
    public class ClientStorageProviderTests : IDisposable
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IClientStorageProvider _storage;
        private readonly StandardData _standardData;

        public ClientStorageProviderTests()
        {
            _contextFactory = new TestDbContextFactory(nameof(ClientStorageProviderTests));
            _storage = new ClientStorageProvider(_contextFactory);
            _standardData = new StandardData();
        }

        public void Dispose()
        {
            using var context = _contextFactory.CreateDbContext();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetClient_ClientExists_GetClient()
        {
            using var context = _contextFactory.CreateDbContext();
            context.Clients.Add(_standardData.ModelClient);
            await context.SaveChangesAsync();

            var result = await _storage.GetClient(_standardData.ModelClient.Id);

            result.Should().BeEquivalentTo(_standardData.ModelClient, options => options
                .Excluding(x => x.Invoices)
                .IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetClient_ClientNotExists_GetNull()
        {
            var result = await _storage.GetClient(1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task AddClient_CorrectData_ClientAdded()
        {
            using var context = _contextFactory.CreateDbContext();

            var client = _standardData.ModelClient;

            var result = await _storage.AddClient(client);

            var addedClient = await context.Clients.SingleOrDefaultAsync(p => p.Id == client.Id);

            using (new AssertionScope())
            {
                result.Should().Be(client.Id);
                addedClient.Should().BeEquivalentTo(client, options => options
                .Excluding(x => x.Invoices)
                .IgnoringCyclicReferences());
            }
        }

        [Fact]
        public async Task DeleteClient_WithExistingClient_ClientDeleted()
        {
            using var context = _contextFactory.CreateDbContext();

            var client = _standardData.ModelClient;

            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();

            var result = await _storage.DeleteClient(client.Id);

            using (new AssertionScope())
            {
                result.Should().BeTrue();
                context.Clients.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task EditClient_WithCorrectData_ClientEdited()
        {
            using var context = _contextFactory.CreateDbContext();

            var client = _standardData.ModelClient;
            var newClient = _standardData.ModelClient2;

            var id = await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();

            var result = await _storage.EditClient(client.Id, newClient);

            using (new AssertionScope())
            {
                using var context2 = _contextFactory.CreateDbContext();

                result.Should().BeTrue();
                (await context2.Clients.FirstOrDefaultAsync()).Should().BeEquivalentTo(newClient, options => options
                .Excluding(x => x.Invoices)
                .IgnoringCyclicReferences());
            }
        }
    }
}
