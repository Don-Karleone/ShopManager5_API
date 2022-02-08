namespace ShopManager5.Api.Services.LoginManagement
{
    public static class LoginManagementServiceRegistrator
    {
        public static void AddLoginManagementService(this IServiceCollection services)
        {
            services.AddSingleton<ILoginManagementService, LoginManagementService>();
        }
    }
}
