using ShopManager5.Api.Data.Models;

namespace ShopManager5.Api.RequestModels.InvoiceElements
{
    public class InvoiceRequest
    {
        public double TotalPrice { get; set; }
        public InvoiceClientRequest Client { get; set; }
        public InvoiceEmployeeRequest Employee { get; set; }
        public List<InvoiceProductRequest> Products { get; set; } = new List<InvoiceProductRequest>();


        public Invoice ToDto()
        {
            return new Invoice
            {
                Date = DateTime.Now,
                PriceTotal = Math.Round(TotalPrice, 2),
                Client = Client.ToDto(),
                Employee = Employee.ToDto(),
                Products = Products?.Select(x => x.ToDto()).ToList()
            };
        }
    }
}
