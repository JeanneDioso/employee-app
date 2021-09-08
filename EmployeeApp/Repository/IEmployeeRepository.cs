using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApp.Models;

namespace EmployeeApp.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> GetEmployee(Guid? employeeId);

        Task<Employee> AddEmployee(Employee employee);

        Task<int> DeleteEmployee(Guid? employeeId);

        Task UpdateEmployee(Employee employee);
    }
}
