using ShopManager5.Api.Data.Models;
using ShopManager5.Data.Storage_providers;

namespace ShopManager5.Api.Services.EmployeesManagement
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly IEmployeeStorageProvider _provider;

        public EmployeeManagementService(IEmployeeStorageProvider provider)
        {
            _provider = provider;
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            return await _provider.AddEmployee(employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _provider.DeleteEmployee(id);
        }

        public async Task<bool> EditEmployee(int id, Employee employee)
        {
            return await _provider.EditEmployee(id, employee);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _provider.GetEmployees();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _provider.GetEmployee(id);
        }
    }
}
