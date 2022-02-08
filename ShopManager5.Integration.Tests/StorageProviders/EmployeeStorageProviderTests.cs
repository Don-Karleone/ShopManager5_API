using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using FluentAssertions.Execution;
using ShopManager5.Integration.Tests.FakeResources;
using ShopManager5.Data.Storage_providers;
using ShopManager5.Api.Data;

namespace ShopManager5.Integration.Tests.StorageProviders
{
    public class EmployeeStorageProviderTests : IDisposable
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IEmployeeStorageProvider _storage;
        private readonly StandardData _standardData;

        public EmployeeStorageProviderTests()
        {
            _contextFactory = new TestDbContextFactory(nameof(EmployeeStorageProviderTests));
            _storage = new EmployeeStorageProvider(_contextFactory);
            _standardData = new StandardData();
        }

        public void Dispose()
        {
            using var context = _contextFactory.CreateDbContext();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetEmployee_EmployeeExists_GetEmployee()
        {
            using var context = _contextFactory.CreateDbContext();

            context.Employees.Add(_standardData.ModelEmployee);
            await context.SaveChangesAsync();

            var result = await _storage.GetEmployee(_standardData.ModelEmployee.Id);

            result.Should().BeEquivalentTo(_standardData.ModelEmployee, options => options
                .Excluding(x => x.Invoices)
                .IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetEmployee_EmployeeNotExists_GetNull()
        {
            var result = await _storage.GetEmployee(1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task AddEmployee_WithCorrectData_EmployeeAdded()
        {
            using var context = _contextFactory.CreateDbContext();

            var employee = _standardData.ModelEmployee;

            var result = await _storage.AddEmployee(employee);

            var addedEmployee = await context.Employees.SingleOrDefaultAsync(p => p.Id == employee.Id);

            using (new AssertionScope())
            {
                result.Should().Be(employee.Id);
                addedEmployee.Should().BeEquivalentTo(employee, options => options
                .Excluding(x => x.Invoices)
                .IgnoringCyclicReferences());
            }
        }

        [Fact]
        public async Task DeleteEmployee_WithExistingEmployee_EmployeeDeleted()
        {
            using var context = _contextFactory.CreateDbContext();
            var employee = _standardData.ModelEmployee;
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();

            var result = await _storage.DeleteEmployee(employee.Id);

            using (new AssertionScope())
            {
                result.Should().BeTrue();
                context.Employees.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task EditEmployee_WithCorrectData_EmployeeEdited()
        {
            using var context = _contextFactory.CreateDbContext();

            var employee = _standardData.ModelEmployee;
            var newEmployee = _standardData.ModelEmployee2;

            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();

            var result = await _storage.EditEmployee(employee.Id, newEmployee);

            using (new AssertionScope())
            {
                result.Should().BeTrue();

                using var context2 = _contextFactory.CreateDbContext();
                (await context2.Employees.FirstOrDefaultAsync()).Should().BeEquivalentTo(newEmployee, options => options
                .Excluding(x => x.Invoices)
                .IgnoringCyclicReferences());
            }
        }
    }
}
