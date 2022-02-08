using ShopManager5.Api.Data.Models;

namespace ShopManager5.Data.Storage_providers
{
    public interface IClientStorageProvider
    {
        Task<List<Client>> GetClients();
        Task<Client> GetClient(int id);
        Task<int> AddClient(Client client);
        Task<bool> EditClient(int id, Client newClient);
        Task<bool> DeleteClient(int id);
    }
}
