using ShopManager5.Api.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopManager5.Api.RequestModels.InvoiceElements
{
    public class InvoiceProductRequest
    {
        public int id { get; set; }
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
                Id = id,
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
