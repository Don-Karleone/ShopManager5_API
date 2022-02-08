using ShopManager5.Api.Data.Models;
using ShopManager5.Data.Storage_providers;

namespace ShopManager5.Api.Services.ProductsManagement
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IProductStorageProvider _ProductStorage;

        public ProductManagementService(IProductStorageProvider ProductStorageProvider)
        {
            _ProductStorage = ProductStorageProvider;
        }

        public async Task<int> AddProduct(Product product)
        {
            return await _ProductStorage.AddProduct(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _ProductStorage.DeleteProduct(id);
        }

        public async Task<bool> EditProduct(int id, Product product)
        {
            return await _ProductStorage.EditProduct(id, product);
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _ProductStorage.GetProduct(id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _ProductStorage.GetProducts();
        }
    }
}
