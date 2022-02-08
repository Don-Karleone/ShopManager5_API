using Microsoft.EntityFrameworkCore;
using ShopManager5.Data.Storage_providers;
using ShopManager5.Api.Data;
using ShopManager5.Api.Data.Models;

namespace ShopManager5.Data.Storage_providers
{
    public class EmployeeStorageProvider:IEmployeeStorageProvider
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public EmployeeStorageProvider(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Employees
                .Include(e => e.Invoices)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Employees
                .Include(e => e.Invoices)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Employees.FirstOrDefaultAsync(e => e.Email.Equals(email));
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            var result = await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<bool> EditEmployee(int id, Employee newEmployee)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            var dbEmployee = await dbContext.Employees
                .Include(e => e.Invoices)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (dbEmployee is null) return false;

            newEmployee.Id = dbEmployee.Id;
            if(newEmployee.PasswordHash is null)
            {
                newEmployee.PasswordHash = dbEmployee.PasswordHash;
            }

            dbContext.CopyEntityMembers(newEmployee, dbEmployee);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            var employee = await dbContext.Employees
                .Include(e => e.Invoices)
                .FirstOrDefaultAsync(employee => employee.Id == id);

            if (employee is null) return false;

            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
