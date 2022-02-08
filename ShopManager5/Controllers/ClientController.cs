using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using ShopManager5.Api.RequestModels;
using ShopManager5.Api.Data.Models;
using ShopManager5.Api.Services.ClientsManagement;

namespace ShopManager5.Api.Controllers
{
    [Route("Api/Clients")]
    [ApiController]
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientManagementService _ClientManagementService;

        public ClientController(IClientManagementService service)
        {
            _ClientManagementService = service;
        }

        [HttpGet]
        public async Task<List<Client>> GetClients()
        {
            return await _ClientManagementService.GetClients();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Client> GetClientById([FromRoute] int id)
        {
            return await _ClientManagementService.GetClient(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddClient([FromBody, Required] ClientRequest body)
        {
            var result = await _ClientManagementService.AddClient(body.ToDto());

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditClient([FromRoute, BindRequired] int id,
            [FromBody, Required] ClientRequest body)
        {
            var result = await _ClientManagementService.EditClient(id, body.ToDto());

            return result ? Ok() : new StatusCodeResult(500);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient([FromRoute, BindRequired] int id)
        {
            var result = await _ClientManagementService.DeleteClient(id);

            return result ? Ok() : new StatusCodeResult(500);
        }
    }
}
