using ShopManager5.Api.Data.Models.ModelConversion;

namespace ShopManager5.Api.Data.Models
{
    public partial class Client : IdentifiableModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? Nip { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
