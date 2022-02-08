using ShopManager5.Api.Data.Models;

namespace ShopManager5.Api.Services.ProductsManagement
{
    public interface IProductManagementService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<int> AddProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<bool> EditProduct(int id, Product product);
    }
}
