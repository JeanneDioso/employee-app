using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApp.Repository;
using EmployeeApp.Models;
using Newtonsoft.Json;

namespace EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await employeeRepository.GetEmployees();
                if (employees == null)
                {
                    return NotFound();
                }

                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("getbyid")]
        public async Task<IActionResult> GetEmployee([FromQuery] Guid employeeId)
        {
            if (employeeId == null)
            {
                return BadRequest();
            }

            try
            {
                var employee = await employeeRepository.GetEmployee(employeeId);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("add")]
        
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    //employee.Id = Guid.NewGuid();

                    var e = await employeeRepository.AddEmployee(employee);
                    if (!e.Equals(null))
                    {
                        return Ok(e);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteEmployee([FromQuery] Guid employeeId)
        {
            int result = 0;

            if (employeeId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await employeeRepository.DeleteEmployee(employeeId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    await employeeRepository.UpdateEmployee(employee);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

    }
}
