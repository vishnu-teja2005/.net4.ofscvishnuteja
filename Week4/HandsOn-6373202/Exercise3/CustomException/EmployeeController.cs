using Microsoft.AspNetCore.Mvc;
using MyEmployeeWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MyEmployeeWebApi.Filters; // Ensure this is present if applying CustomAuthFilter via attribute

namespace MyEmployeeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomAuthFilter] // Keep this if you want it applied
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>();

        public EmployeeController()
        {
            if (!_employees.Any())
            {
                _employees.AddRange(GetStandardEmployeeList());
            }
        }

        private List<Employee> GetStandardEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1, Name = "John Doe", Salary = 75000, Permanent = true,
                    Department = new Department { Id = 101, Name = "IT" },
                    Skills = new List<Skill> { new Skill { Id = 1, Name = "C#" }, new Skill { Id = 2, Name = "ASP.NET Core" } },
                    DateOfBirth = new DateTime(1985, 3, 15)
                },
                new Employee
                {
                    Id = 2, Name = "Jane Smith", Salary = 85000, Permanent = true,
                    Department = new Department { Id = 102, Name = "HR" },
                    Skills = new List<Skill> { new Skill { Id = 3, Name = "Recruitment" }, new Skill { Id = 4, Name = "Employee Relations" } },
                    DateOfBirth = new DateTime(1990, 7, 22)
                },
                new Employee
                {
                    Id = 3, Name = "Peter Jones", Salary = 60000, Permanent = false,
                    Department = new Department { Id = 101, Name = "IT" },
                    Skills = new List<Skill> { new Skill { Id = 5, Name = "JavaScript" }, new Skill { Id = 6, Name = "React" } },
                    DateOfBirth = new DateTime(1992, 11, 5)
                }
            };
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Employee>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)] // Add this line for Swagger documentation
        public ActionResult<IEnumerable<Employee>> Get()
        {
            // Throw an exception here for testing the CustomExceptionFilter
            throw new InvalidOperationException("An intentional error occurred during employee retrieval for demonstration.");

            // The line below will not be reached due to the exception above.
            // return Ok(_employees);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }
            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Employee> Post([FromBody] Employee newEmployee)
        {
            if (newEmployee == null)
            {
                return BadRequest("Employee data is null.");
            }
            if (string.IsNullOrWhiteSpace(newEmployee.Name) || newEmployee.Salary <= 0)
            {
                return BadRequest("Invalid employee data provided (Name or Salary missing/invalid).");
            }

            newEmployee.Id = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;
            _employees.Add(newEmployee);

            return CreatedAtAction(nameof(Get), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Put(int id, [FromBody] Employee updatedEmployee)
        {
            if (updatedEmployee == null || updatedEmployee.Id != id)
            {
                return BadRequest("Employee ID mismatch or invalid data.");
            }

            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.Salary = updatedEmployee.Salary;
            existingEmployee.Permanent = updatedEmployee.Permanent;
            existingEmployee.Department = updatedEmployee.Department;
            existingEmployee.Skills = updatedEmployee.Skills;
            existingEmployee.DateOfBirth = updatedEmployee.DateOfBirth;

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Delete(int id)
        {
            var employeeToDelete = _employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }
            _employees.Remove(employeeToDelete);
            return NoContent();
        }

        [HttpGet("standard")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<Employee>> GetStandard()
        {
            return Ok(GetStandardEmployeeList());
        }
    }
}