using BankManagementSystem.Entity.Models;
using BankManagementSystem.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeRepository _accountTypeRepository;

        public AccountTypeController(IAccountTypeRepository accountTypeRepository)
        {
            _accountTypeRepository = accountTypeRepository;
        }

        [HttpGet("GetAllAccountTypes")]
        public async Task<IActionResult> GetAllAccountTypes()
        {
            var accountTypes = await _accountTypeRepository.GetAllAccountTypesAsync();
            if (accountTypes == null)
            {
                return NotFound();
            }
            return Ok(accountTypes);
        }

        [HttpGet("GetAccountTypeById")]
        public async Task<IActionResult> GetAccountTypeById(int id)
        {
            var accountType = await _accountTypeRepository.GetAccountTypeByIdAsync(id);
            if (accountType == null)
            {
                return NotFound();
            }
            return Ok(accountType);
        }

        [HttpPost("AddAccountType")]
        public async Task<IActionResult> AddAccountType([FromBody] AccountType accountType)
        {
            await _accountTypeRepository.AddAccountTypeAsync(accountType);
            return Ok();
        }

        [HttpPut("UpdateAccountType")]
        public async Task<IActionResult> UpdateAccountType([FromBody] AccountType accountType)
        {
            await _accountTypeRepository.UpdateAccountTypeAsync(accountType);
            return Ok();
        }

        [HttpDelete("DeleteAccountType")]
        public async Task<IActionResult> DeleteAccountType(int id)
        {
            await _accountTypeRepository.DeleteAccountTypeAsync(id);
            return Ok();
        }
    }
}
