using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ShopManager5.Api.Controllers.GuiDtoModels;
using ShopManager5.Api.Data.Models.DtoModels;
using ShopManager5.Data.Storage_providers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopManager5.Api.Services.LoginManagement
{
    public class LoginManagementService : ILoginManagementService
    {
        private readonly IEmployeeStorageProvider _employeeStorage;
        private readonly IMapper _mapper;

        public LoginManagementService(IEmployeeStorageProvider employeeStorage, IMapper mapper)
        {
            _employeeStorage = employeeStorage;
            _mapper = mapper;
        }

        public async Task<DtoEmployee> AuthenticateEmployee(EmployeeLogin credentials)
        {
            var employee = await _employeeStorage.GetEmployeeByEmail(credentials.Email);
            if (employee is null)
                return null;

            var passwordHash = EmployeeLogin.HashPassword(credentials.Password);

            if (passwordHash.Length != employee.PasswordHash.Length)
                return null;

            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != employee.PasswordHash[i])
                    return null;
            }

            return _mapper.Map<DtoEmployee>(employee);
        }

        public string GenerateToken(DtoEmployee employee)
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = configBuilder.Build();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.
                GetSection("Jwt:SecurityKey").Get<string>()));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: config.GetSection("Jwt:Issuer").Get<string>(),
                audience: config.GetSection("Jwt:Audience").Get<string>(),
                claims: new List<Claim>
                {
                    new Claim("Id", employee.Id.ToString()),
                    new Claim("FirstName", employee.FirstName),
                    new Claim("Email", employee.Email),
                    new Claim("Role", employee.Role),
                    new Claim(ClaimTypes.Role, employee.Role)
                },
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
