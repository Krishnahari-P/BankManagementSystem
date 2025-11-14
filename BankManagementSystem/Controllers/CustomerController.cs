using BankManagementSystem.Entity.Dto;
using BankManagementSystem.Entity.Models;
using BankManagementSystem.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes ="Basic")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #region CRUD

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpGet("GetCustomerBySearch")]
        public async Task<IActionResult> GetCustomerBySearch(string? customerName, string? aadhar, string? status, string? phone)
        {
            var customer = await _customerRepository.GetCustomerBySearchAsync(customerName,aadhar,status,phone);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            await _customerRepository.AddCustomerAsync(customer);
            return Ok();
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            await _customerRepository.UpdateCustomerAsync(customer);
            return Ok();
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
            return Ok();
        }
        #endregion
        #region Register as a customer
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CustomerRegistrationRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCustomer = await _customerRepository.GetExistingCustomerAsync(model.AadharNumber, model.PAN, model.Phone);
            if (existingCustomer != null)
            {
                return BadRequest(new
                {
                    Message = "A customer with the same Aadhar, PAN, or Phone number already exists.",
                    ExistingCustomerId = existingCustomer.CustomerId
                });
            }

            var customer = new Customer
            {
                CustomerName = model.CustomerName,
                DateOfBirth = model.DateOfBirth.Value,
                Occupation = model.Occupation,
                Phone = model.Phone,
                AadharNumber = model.AadharNumber,
                PAN = model.PAN,
            };
            await _customerRepository.AddCustomerAsync(customer);
            return Ok(new { Message = "Registration submitted successfully. Await admin approval." });
        }
        #endregion
    }
}
