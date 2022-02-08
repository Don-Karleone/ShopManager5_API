using System.ComponentModel.DataAnnotations;
using System.Text;
using XSystem.Security.Cryptography;

namespace ShopManager5.Api.Data.Models.DtoModels
{
    public class EmployeeLogin
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }

        public static byte[] HashPassword(string Password)
        {
            var UE = new UnicodeEncoding();
            byte[] passwordBytes = UE.GetBytes(Password);
            var SHhash = new SHA1Managed();
            return SHhash.ComputeHash(passwordBytes);
        }
    }
}
