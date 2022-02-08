using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManager5.Api.Data.Models.DtoModels;
using ShopManager5.Api.Services.LoginManagement;

namespace ShopManager5.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ILoginManagementService _loginService;

        public AuthController(ILoginManagementService loginManagementService)
        {
            _loginService = loginManagementService;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] EmployeeLogin credentials)
        {
            var employee = await _loginService.AuthenticateEmployee(credentials);
            if (employee is null)
                return NotFound();

            var tokenString = _loginService.GenerateToken(employee);

            return Ok(new { Token = tokenString });
        }
    }
}
