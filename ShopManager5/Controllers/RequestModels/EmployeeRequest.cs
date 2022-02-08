using ShopManager5.Api.Data.Models;
using ShopManager5.Api.Data.Models.DtoModels;
using System.ComponentModel.DataAnnotations;

namespace ShopManager5.Api.RequestModels
{
    public class EmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string? Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public int State { get; set; }

        public Employee ToDto()
        {
            byte[]? passwordHash;
            if (Password != null)
            {
                passwordHash = EmployeeLogin.HashPassword(Password);
            }
            else passwordHash = null;

            return new Employee
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PasswordHash = passwordHash,
                PhoneNumber = PhoneNumber,
                State = (State)State,
                Role = Role
            };
        }
    }
}
