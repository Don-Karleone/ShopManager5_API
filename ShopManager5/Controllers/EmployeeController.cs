using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using ShopManager5.Api.RequestModels;
using ShopManager5.Api.Services.EmployeesManagement;
using AutoMapper;
using System.Security.Claims;
using ShopManager5.Api.Data.Models;

namespace ShopManager5.Api.Controllers
{
    [Route("Api/Employees")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeManagementService _EmployeeManagementService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeManagementService service, IMapper mapper)
        {
            _EmployeeManagementService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<GuiDtoModels.DtoEmployee>> GetEmployees()
        {
            var employees = await _EmployeeManagementService.GetEmployees();
            return _mapper.Map<List<GuiDtoModels.DtoEmployee>>(employees);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Employee> GetEmployeeById([FromRoute] int id)
        {
            return await _EmployeeManagementService.GetEmployee(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody, Required] EmployeeRequest body)
        {
            var result = await _EmployeeManagementService.AddEmployee(body.ToDto());

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditEmployee([FromRoute, BindRequired] int id,
            [FromBody, Required] EmployeeRequest body)
        {
            var result = await _EmployeeManagementService.EditEmployee(id, body.ToDto());

            return result ? Ok() : new StatusCodeResult(500);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee([FromRoute, BindRequired] int id)
        {
            var result = await _EmployeeManagementService.DeleteEmployee(id);

            return result ? Ok() : new StatusCodeResult(500);
        }

        private GuiDtoModels.DtoEmployee GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity is null)
                return null;

            var userClaims = identity.Claims;
            return new GuiDtoModels.DtoEmployee
            {
                FirstName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
            };
        }
    }
}
