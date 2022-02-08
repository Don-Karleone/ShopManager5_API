using ShopManager5.Data.Storage_providers;

namespace ShopManager5.Api.Services.ClientsManagement
{
    public static class ClientManagementServiceRegistrator
    {
        public static void AddClientManagementService(this IServiceCollection services)
        {
            services.AddSingleton<IClientManagementService, ClientManagementService>();
            services.AddSingleton<IClientStorageProvider, ClientStorageProvider>();
        }
    }
}
