using ShopManager5.Api.Data.Models.ModelConversion;

namespace ShopManager5.Api.Data.Models
{
    public partial class Product : IdentifiableModel
    {
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public List<Invoice> Invoices { get; set; }
        public double Price { get; set; }
    }
}
