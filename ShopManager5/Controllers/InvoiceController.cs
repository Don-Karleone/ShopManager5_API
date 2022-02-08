using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using ShopManager5.Api.RequestModels;
using ShopManager5.Api.Data.Models;
using ShopManager5.Api.Services.InvoicesManagement;
using ShopManager5.Api.RequestModels.InvoiceElements;

namespace ShopManager5.Api.Controllers
{
    [Route("Api/Invoices")]
    [ApiController]
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceManagementService _InvoiceManagementService;

        public InvoiceController(IInvoiceManagementService service)
        {
            _InvoiceManagementService = service;
        }

        [HttpGet]
        public async Task<List<Invoice>> GetInvoices()
        {
            return await _InvoiceManagementService.GetInvoices();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Invoice> GetInvoiceById([FromRoute] int id)
        {
            return await _InvoiceManagementService.GetInvoice(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddInvoice([FromBody, Required] InvoiceRequest body)
        {
            var result = await _InvoiceManagementService.AddInvoice(body.ToDto());

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditInvoice([FromRoute, BindRequired] int id,
            [FromBody, Required] InvoiceRequest body)
        {
            var result = await _InvoiceManagementService.EditInvoice(id, body.ToDto());

            return result ? Ok() : new StatusCodeResult(500);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInvoice([FromRoute, BindRequired] int id)
        {
            var result = await _InvoiceManagementService.DeleteInvoice(id);

            return result ? Ok() : new StatusCodeResult(500);
        }
    }
}
