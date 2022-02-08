using ShopManager5.Api.Controllers.GuiDtoModels;
using ShopManager5.Api.Data.Models.DtoModels;

namespace ShopManager5.Api.Services.LoginManagement
{
    public interface ILoginManagementService
    {
        Task<DtoEmployee> AuthenticateEmployee(EmployeeLogin credentials);
        string GenerateToken(DtoEmployee employee);
    }
}
