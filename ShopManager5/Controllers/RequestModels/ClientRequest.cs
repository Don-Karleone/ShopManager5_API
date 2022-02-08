using ShopManager5.Api.Data.Models;
using ShopManager5.Api.Data.Models.DtoModels;
using System.ComponentModel.DataAnnotations;

namespace ShopManager5.Api.RequestModels
{
    public class ClientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? Nip { get; set; }


        public Client ToDto()
        {
            return new Client
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                DateOfBirth = DateOfBirth,
                City = City,
                Street = Street,
                BuildingNumber = BuildingNumber,
                CompanyName = CompanyName,
                Nip = Nip
            };
        }
    }
}
