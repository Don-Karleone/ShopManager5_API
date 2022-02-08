using ShopManager5.Api.Data.Models;

namespace ShopManager5.Api.Services.ClientsManagement
{
    public interface IClientManagementService
    {
        Task<List<Client>> GetClients();
        Task<Client> GetClient(int id);
        Task<int> AddClient(Client client);
        Task<bool> DeleteClient(int id);
        Task<bool> EditClient(int id, Client client);
    }
}
