using ShopManager5.Api.Data.Models;

namespace ShopManager5.Data.Storage_providers
{
    public interface IProductStorageProvider
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<int> AddProduct (Product product);
        Task<bool> DeleteProduct(int id);
        Task<bool> EditProduct(int id, Product product);
    }
}
