using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using ShopManager5.Api.Services.ProductsManagement;
using ShopManager5.Api.RequestModels;
using ShopManager5.Api.Data.Models;

namespace ShopManager5.Api.Controllers
{
    [Route("Api/Products")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductManagementService _ProductManagementService;

        public ProductController(IProductManagementService service)
        {
            _ProductManagementService = service;
        }

        [HttpGet]
        public async Task<List<Product>> GetProducts()
        {

            return await _ProductManagementService.GetProducts();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Product> GetProductById([FromRoute] int id)
        {
            return await _ProductManagementService.GetProduct(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody, Required] ProductRequest body)
        {
            var result = await _ProductManagementService.AddProduct(body.ToDto());

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditProduct([FromRoute, BindRequired] int id,
            [FromBody, Required] ProductRequest body)
        {
            var result = await _ProductManagementService.EditProduct(id, body.ToDto());

            return result ? Ok() : new StatusCodeResult(500);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute, BindRequired] int id)
        {
            var result = await _ProductManagementService.DeleteProduct(id);

            return result ? Ok() : new StatusCodeResult(500);
        }
    }
}
