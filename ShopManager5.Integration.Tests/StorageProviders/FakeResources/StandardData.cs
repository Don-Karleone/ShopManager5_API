using ShopManager5.Api.Data.Models;
using ShopManager5.Api.Data.Models.DtoModels;
using System;
using System.Collections.Generic;

namespace ShopManager5.Integration.Tests.FakeResources
{
    public class StandardData
    {
        public readonly Product ModelProduct;
        public readonly Product ModelProduct2;
        public readonly Client ModelClient;
        public readonly Client ModelClient2;
        public readonly Employee ModelEmployee;
        public readonly Employee ModelEmployee2;
        public readonly Invoice ModelInvoice;
        public readonly Invoice ModelInvoice2;

        public StandardData()
        {
            ModelProduct = new()
            {
                Id = 0,
                Category = "PC",
                Brand = "Dell",
                Model = "7020",
                Description = "i5-4590, HDD 500GB",
                Quantity = 5,
                Price = 799.99,
            };

            ModelProduct2 = new()
            {
                Id = 1,
                Category = "LCD",
                Brand = "Dell",
                Model = "7020",
                Description = "i5-4590, HDD 500GB",
                Quantity = 5,
                Price = 999.99,
            };

            ModelClient = new()
            {
                Id = 0,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "123456789",
                Email = "Test@mail.com",
                City = "TestCity",
                Street = "TestStreet",
                BuildingNumber = "10",
                CompanyName = "TestCompanyName",
                Nip = "TestNIP",
                Invoices = new List<Invoice>(),
            };

            ModelClient2 = new()
            {
                Id = 1,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "123456789",
                Email = "Test@mail.com",
                City = "TestCity",
                Street = "TestStreet",
                BuildingNumber = "10",
                CompanyName = "TestAnotherCompany",
                Nip = "TestNIP",
                Invoices = new List<Invoice>()
            };

            ModelEmployee = new()
            {
                Id = 0,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "Test@mail.com",
                PasswordHash = EmployeeLogin.HashPassword("TestPasswordHash"),
                PhoneNumber = "123456789",
                State = State.Active,
                Invoices = new List<Invoice>(),
                Role = "Admin"
            };

            ModelEmployee2 = new()
            {
                Id = 1,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "Test@mail.com",
                PasswordHash = EmployeeLogin.HashPassword("TestPasswordHash"),
                PhoneNumber = "123456789",
                State = State.Archived,
                Invoices = new List<Invoice>(),
                Role = "Seller"
            };

            ModelInvoice = new()
            {
                Id = 0,
                Date = new DateTime(1990, 1, 1),
                PriceTotal = 799.99,
                Client = ModelClient,
                Employee = ModelEmployee,
                Products = new List<Product>()
                {
                    ModelProduct
                }
            };

            ModelInvoice2 = new()
            {
                Date = new DateTime(1990, 1, 1),
                PriceTotal = 799.99,
                Client = ModelClient2,
                Employee = ModelEmployee,
                Products = new List<Product>()
                {
                    ModelProduct
                }
            };
        }
    }
}
