using BankManagementSystem.Entity.Security;
using BankManagementSystem.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdministrativeController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AdministrativeController(ICustomerRepository customerRepository,UserManager<ApplicationUser> userManager,IAccountTypeRepository accountTypeRepository,IAccountRepository accountRepository,ITransactionRepository transactionRepository)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
            _accountTypeRepository = accountTypeRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
        #region Customer creation handling
        [HttpPut("ApproveCustomer")]
        public async Task<IActionResult> ApproveCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            customer.Status = "Approved";
            customer.ApprovalDate = DateTime.Now;
            await _customerRepository.UpdateCustomerAsync(customer);

            return Ok(new { Message = "Customer approved successfully." });
        }
        [HttpPut("RejectCustomer")]
        public async Task<IActionResult> RejectCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            customer.Status = "Rejected";
            customer.ApprovalDate = DateTime.Now;
            await _customerRepository.UpdateCustomerAsync(customer);

            return Ok(new { Message = "Customer Rejected" });
        }

        [HttpPost("CreateCustomerAccount")]
        public async Task<IActionResult> CreateCustomerAccount(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            if (customer.Status != "Approved")
            {
                return BadRequest("Customer must be approved before creating an account.");

            }
            string username = $"{customer.CustomerName}{customer.CustomerId}@bank.com";
            string password = $"{customer.CustomerName}@123";
            var user = new ApplicationUser
            {
                UserName = username,
                Email = username,
                Status = "Approved",
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            customer.ApplicationUserID = user.Id;
            await _customerRepository.UpdateCustomerAsync(customer);

            return Ok(new { Message = "Customer account created successfully.", Username = username, TemporaryPassword = password });
        }
        #endregion

        #region Account creation request handling
        [HttpPut("ApproveAccount")]
        public async Task<IActionResult> ApproveAccount(int accountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
                return NotFound("Account not found.");

            if (account.Status != "Pending")
                return BadRequest("Only pending accounts can be approved.");

            account.Status = "Active";
            await _accountRepository.UpdateAccountAsync(account);

            return Ok(new { Message = "Account approved successfully.", AccountNumber = account.AccountNumber });
        }

        [HttpPut("RejectAccount")]
        public async Task<IActionResult> RejectAccount(int accountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
                return NotFound("Account not found.");

            account.Status = "Rejected";
            await _accountRepository.UpdateAccountAsync(account);

            return Ok(new { Message = "Account request rejected." });
        }
        #endregion

        #region Transaction handling

        [HttpGet("GetPendingTransactions")]
        public async Task<IActionResult> GetPendingTransactions()
        {
            var transactions = await _transactionRepository.GetPendingTransactionsAsync();
            return Ok(transactions);
        }

        [HttpPut("ApproveDeposit")]
        public async Task<IActionResult> ApproveDeposit(int transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            if (transaction == null)
                return NotFound("Transaction not found.");

            if (transaction.TransactionType != "Deposit")
                return BadRequest("This transaction is not a deposit.");

            if (transaction.Status != "Pending")
                return BadRequest("Only pending transactions can be approved.");

            var account = await _accountRepository.GetAccountByIdAsync(transaction.AccountId);
            if (account == null)
                return NotFound("Account not found.");

            account.Balance += transaction.Amount;
            await _accountRepository.UpdateAccountAsync(account);

            transaction.Status = "Approved";
            transaction.TransactionDate = DateTime.Now.Date;
            await _transactionRepository.UpdateTransactionAsync(transaction);

            return Ok(new { Message = "Deposit approved successfully.", NewBalance = account.Balance });
        }

        [HttpPut("ApproveWithdraw")]
        public async Task<IActionResult> ApproveWithdraw(int transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            if (transaction == null)
                return NotFound("Transaction not found.");

            if (transaction.TransactionType != "Withdraw")
                return BadRequest("This transaction is not a withdrawal.");

            if (transaction.Status != "Pending")
                return BadRequest("Only pending transactions can be approved.");

            var account = await _accountRepository.GetAccountByIdAsync(transaction.AccountId);
            if (account == null)
                return NotFound("Account not found.");

            if (account.Balance < transaction.Amount)
                return BadRequest("Insufficient balance to approve withdrawal.");

            account.Balance -= transaction.Amount;
            await _accountRepository.UpdateAccountAsync(account);

            transaction.Status = "Approved";
            transaction.TransactionDate = DateTime.Now.Date;
            await _transactionRepository.UpdateTransactionAsync(transaction);

            return Ok(new { Message = "Withdrawal approved successfully.", NewBalance = account.Balance });
        }

        [HttpPut("RejectTransaction")]
        public async Task<IActionResult> RejectTransaction(int transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            if (transaction == null)
                return NotFound("Transaction not found.");

            if (transaction.Status != "Pending")
                return BadRequest("Only pending transactions can be rejected.");

            transaction.Status = "Rejected";
            transaction.TransactionDate = DateTime.Now.Date;
            await _transactionRepository.UpdateTransactionAsync(transaction);

            return Ok(new { Message = "Transaction rejected successfully." });
        }
        #endregion
    }
}
