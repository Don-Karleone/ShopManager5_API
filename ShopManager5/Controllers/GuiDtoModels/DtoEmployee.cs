using DataAnnotationsExtensions;
using ShopManager5.Api.Data.Models;

namespace ShopManager5.Api.Controllers.GuiDtoModels
{
    public class DtoEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Email]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public State State { get; set; }
        public string Role { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
