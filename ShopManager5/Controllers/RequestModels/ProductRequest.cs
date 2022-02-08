using ShopManager5.Api.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopManager5.Api.RequestModels
{
    public class ProductRequest
    {
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public Product ToDto()
        {
            return new Product()
            {
                Category = Category,
                Brand = Brand,
                Model = Model,
                Description = Description,
                Quantity = Quantity,
                Price = Price
            };
        }
    }
}
