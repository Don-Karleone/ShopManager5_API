using ShopManager5.Api.Data.Models;
using ShopManager5.Data.Storage_providers;

namespace ShopManager5.Api.Services.ClientsManagement
{
    public class ClientManagementService : IClientManagementService
    {
        private readonly IClientStorageProvider _provider;

        public ClientManagementService(IClientStorageProvider provider)
        {
            _provider = provider;
        }

        public async Task<int> AddClient(Client client)
        {
            return await _provider.AddClient(client);
        }

        public async Task<bool> DeleteClient(int id)
        {
            return await _provider.DeleteClient(id);
        }

        public async Task<bool> EditClient(int id, Client client)
        {
            return await _provider.EditClient(id, client);
        }

        public async Task<List<Client>> GetClients()
        {
            return await _provider.GetClients();
        }

        public async Task<Client> GetClient(int id)
        {
            return await _provider.GetClient(id);
        }
    }
}
