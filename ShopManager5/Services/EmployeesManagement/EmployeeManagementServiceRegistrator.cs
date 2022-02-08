using ShopManager5.Data.Storage_providers;

namespace ShopManager5.Api.Services.EmployeesManagement
{
    public static class EmployeeManagementServiceRegistrator
    {
        public static void AddEmployeeManagementService(this IServiceCollection services)
        {
            services.AddSingleton<IEmployeeManagementService, EmployeeManagementService>();
            services.AddSingleton<IEmployeeStorageProvider, EmployeeStorageProvider>();
        }
    }
}
