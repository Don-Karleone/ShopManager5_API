using ShopManager5.Data.Storage_providers;

namespace ShopManager5.Api.Services.ProductsManagement
{
    public static class ProductsManagementServiceRegistrator
    {
        public static void AddProductManagementService(this IServiceCollection services)
        {
            services.AddSingleton<IProductStorageProvider, ProductStorageProvider>();
            services.AddSingleton<IProductManagementService, ProductManagementService>();
        }
    }
}
