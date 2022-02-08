using ShopManager5.Api.Data.Models.ModelConversion;

namespace ShopManager5.Api.Data.Models
{
    public partial class Invoice : IdentifiableModel
    {
        public DateTime Date { get; set; }
        public double PriceTotal { get; set; }
        public Client Client { get; set; }
        public Employee Employee { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
