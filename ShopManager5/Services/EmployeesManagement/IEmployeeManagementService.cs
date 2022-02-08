using ShopManager5.Api.Data.Models;

namespace ShopManager5.Api.Services.EmployeesManagement
{
    public interface IEmployeeManagementService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<int> AddEmployee(Employee employee);
        Task<bool> DeleteEmployee(int id);
        Task<bool> EditEmployee(int id, Employee employee);
    }
}
