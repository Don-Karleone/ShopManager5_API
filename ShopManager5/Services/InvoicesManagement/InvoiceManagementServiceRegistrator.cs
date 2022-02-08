using ShopManager5.Data.Storage_providers;

namespace ShopManager5.Api.Services.InvoicesManagement
{
    public static class InvoiceManagementServiceRegistrator
    {
        public static void AddInvoiceManagementService(this IServiceCollection services)
        {
            services.AddSingleton<IInvoiceManagementService, InvoiceManagementService>();
            services.AddSingleton<IInvoiceStorageProvider, InvoiceStorageProvider>();
        }
    }
}
