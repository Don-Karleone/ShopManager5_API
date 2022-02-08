using DataAnnotationsExtensions;
using ShopManager5.Api.Data.Models.ModelConversion;

namespace ShopManager5.Api.Data.Models
{
    public partial class Employee : IdentifiableModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Email]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public State State { get; set; }
        public string Role { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
