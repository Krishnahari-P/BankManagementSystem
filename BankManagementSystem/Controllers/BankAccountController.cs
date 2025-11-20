using BankManagementSystem.Entity.Dto;
using BankManagementSystem.Entity.Models;
using BankManagementSystem.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankAccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountTypeRepository _accountTypeRepository;

        public BankAccountController(IAccountRepository accountRepository, ICustomerRepository customerRepository,IAccountTypeRepository accountTypeRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _accountTypeRepository = accountTypeRepository;
        }
        #region CRUD
        [HttpGet("GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            if (accounts == null)
            {
                return NotFound();
            }
            return Ok(accounts);
        }

        [HttpGet("GetAccountById")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost("AddAccount")]
        public async Task<IActionResult> AddAccount([FromBody] Account account)
        {
            await _accountRepository.AddAccountAsync(account);
            return Ok();
        }

        [HttpPut("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] Account account)
        {
            await _accountRepository.UpdateAccountAsync(account);
            return Ok();
        }

        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountRepository.DeleteAccountAsync(id);
            return Ok();
        }
        #endregion
        #region AccountCreationRequest

        [HttpPost("RequestAccount")]
        public async Task<IActionResult> RequestAccount([FromBody] AccountRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _customerRepository.GetCustomerByIdAsync(model.CustomerId);
            if (customer == null)
                return NotFound("Customer not found.");

            if (customer.Status != "Approved")
                return BadRequest("Customer must be approved before requesting an account.");

            var accountTypes = await _accountTypeRepository.GetAllAccountTypesAsync();
            var accountType = accountTypes.FirstOrDefault(a => a.AccountTypeId == model.AccountTypeId);
            if (accountType == null)
                return NotFound("Invalid account type.");

            if (accountType.TypeName == "Fixed Deposit" && model.InitialDeposit < 3000)
                return BadRequest("Minimum balance for Fixed Deposit is 3000.");

            if ((accountType.TypeName == "Savings" || accountType.TypeName == "Current" || accountType.TypeName=="Fixed Deposit") && model.InitialDeposit < 0)
                return BadRequest("Initial deposit cannot be negative.");

            var account = new Account
            {
                CustomerId = model.CustomerId,
                AccountTypeId = model.AccountTypeId,
                Balance = model.InitialDeposit,
                Status = "Pending",
                CreatedDate = DateTime.Now.Date,
                AccountNumber = Guid.NewGuid().ToString("N")[..12].ToUpper()
            };

            await _accountRepository.AddAccountAsync(account);
            return Ok(new { Message = "Account request submitted. Await manager approval." });
        }
        #endregion
        #region GetAccountByCustomerId
        [HttpGet("GetByCustomer")]
        public async Task<IActionResult> GetByCustomer(int customerId)
        {
            var accounts = await _accountRepository.GetAccountsByCustomerIdAsync(customerId);
            if (accounts == null || !accounts.Any())
                return NotFound("No accounts found for this customer.");

            var result = accounts.Select(a => new
            {
                a.AccountId,
                a.AccountNumber,
                a.AccountTypeId,
                AccountType = a.AccountTypeSet.TypeName,
                a.Balance,
                a.Status,
                CreatedDate = a.CreatedDate.ToString("yyyy-MM-dd")
            });

            return Ok(result);
        }
        #endregion
        #region GetAccountByAccountNumber
        [HttpGet("GetAccountByAccountNumber")]
        public async Task<IActionResult> GetAccountByAccountNumber(string accountNumber)
        {
            var account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
            if (account == null)
                return NotFound("No account found for this account number");
            return Ok(account);
        }
        #endregion
    }
}
