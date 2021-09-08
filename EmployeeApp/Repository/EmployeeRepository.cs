using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeRecordsContext db;
        public EmployeeRepository(EmployeeRecordsContext _db)
        {
            db = _db;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            if (db != null)
            {
                return await db.Employees.ToListAsync();
            }

            return null;
        }


        public async Task<Employee> GetEmployee(Guid? employeeId)
        {
            if (db != null)
            {
                return await (from e in db.Employees
                              where e.Id == employeeId
                              select new Employee
                              {
                                  Id = e.Id,
                                  FirstName = e.FirstName,
                                  MiddleName = e.MiddleName,
                                  LastName = e.LastName
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            if (db != null)
            {
                await db.Employees.AddAsync(employee);
                await db.SaveChangesAsync();

                return employee;
            }

            return null;
        }

        public async Task<int> DeleteEmployee(Guid? employeeId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the employee for specific id
                var employee = await db.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);

                if (employee != null)
                {
                    //Delete
                    db.Employees.Remove(employee);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateEmployee(Employee employee)
        {
            if (db != null)
            {
                //Update that employee
                db.Employees.Update(employee);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }
}
