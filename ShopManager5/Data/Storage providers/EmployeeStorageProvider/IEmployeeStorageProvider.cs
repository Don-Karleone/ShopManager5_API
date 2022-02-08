using ShopManager5.Api.Data.Models;

namespace ShopManager5.Data.Storage_providers
{
    public interface IEmployeeStorageProvider
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<int> AddEmployee(Employee employee);
        Task<bool> EditEmployee(int id, Employee newEmployee);
        Task<bool> DeleteEmployee(int id);
    }
}
