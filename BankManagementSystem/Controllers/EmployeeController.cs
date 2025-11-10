using BankManagementSystem.Entity.Models;
using BankManagementSystem.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Basic")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpGet("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _employeeRepository.AddEmployeeAsync(employee);
            return Ok();
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            await _employeeRepository.UpdateEmployeeAsync(employee);
            return Ok();
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return Ok();
        }
    }
}
