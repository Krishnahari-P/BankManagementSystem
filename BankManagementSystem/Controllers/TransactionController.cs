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
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;

        public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository,ICustomerRepository customerRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }
        #region CRUD
        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        [HttpGet("GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpPost("AddTransaction")]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            await _transactionRepository.AddTransactionAsync(transaction);
            return Ok();
        }

        [HttpPut("UpdateTransaction")]
        public async Task<IActionResult> UpdateTransaction([FromBody] Transaction transaction)
        {
            await _transactionRepository.UpdateTransactionAsync(transaction);
            return Ok();
        }

        [HttpDelete("DeleteTransaction")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            await _transactionRepository.DeleteTransactionAsync(id);
            return Ok();
        }
        #endregion

        #region Deposit

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _accountRepository.GetAccountByIdAsync(model.AccountId);
            if (account == null)
                return NotFound("Account not found.");

            if (account.Status != "Active")
                return BadRequest("Account must be active to perform transactions.");

            var transaction = new Transaction
            {
                AccountId = account.AccountId,
                TransactionType = "Deposit",
                Amount = model.Amount,
                TransactionDate = DateTime.Now.Date,
                Description = model.Description ?? "Deposit transaction",
                Status = "Pending"
            };

            await _transactionRepository.AddTransactionAsync(transaction);

            return Ok(new
            {
                Message = "Deposit request submitted successfully. Awaiting manager approval.",
                TransactionId = transaction.TransactionId,
                Status = transaction.Status
            });
        }
        #endregion

        #region Withdraw

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _accountRepository.GetAccountByIdAsync(model.AccountId);
            if (account == null)
                return NotFound("Account not found.");

            if (account.Status != "Active")
                return BadRequest("Account must be active to perform transactions.");

            if (account.Balance < model.Amount)
                return BadRequest("Insufficient balance to request withdrawal.");

            var transaction = new Transaction
            {
                AccountId = account.AccountId,
                TransactionType = "Withdraw",
                Amount = model.Amount,
                TransactionDate = DateTime.Now.Date,
                Description = model.Description ?? "Withdrawal transaction",
                Status = "Pending",
            };

            await _transactionRepository.AddTransactionAsync(transaction);

            return Ok(new
            {
                Message = "Withdrawal request submitted successfully. Awaiting manager approval.",
                TransactionId = transaction.TransactionId,
                Status = transaction.Status
            });
        }

        #endregion

        #region GetTransactions
        [HttpGet("GetTransactionsByAccount")]
        public async Task<IActionResult> GetTransactionsByAccount(int accountId)
        {
            var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
            if (transactions == null || !transactions.Any())
                return NotFound("No transactions found for this account.");

            return Ok(transactions);
        }

        [HttpGet("GetTransactionsBySearch")]
        public async Task<IActionResult> GetTransactionsBySearch(string? status, DateTime? date)
        {
            var transactions = await _transactionRepository.GetTransactionBySearchAsync(status, date);
            if (transactions == null || !transactions.Any())
                return NotFound("No transactions found");

            return Ok(transactions);
        }

        [HttpGet("GetTransactionsByCustomerId")]
        public async Task<IActionResult> GetTransactionsByCustomerId(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer.Status == "Rejected")
            {
                return NotFound("Customer status is rejected");
            }
            var transactions = await _transactionRepository.GetTransactionsByCustomerIdAsync(customerId);           
            if (transactions == null || !transactions.Any())
                return NotFound("No transactions found for this customer.");

            return Ok(transactions);
        }

        #endregion

        #region Transfer
        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sender = await _accountRepository.GetAccountByIdAsync(model.SenderAccountId);
            if (sender == null)
            {
                return NotFound("Sender account not found.");
            }

            if (sender.Status != "Active")
            {
                return BadRequest("Sender account must be active to perform transfers.");
            }

            if (sender.Status == "Freezed")
            {
                return BadRequest("Sender account is freezed");
            }

            if (sender.Balance < model.Amount)
            {
                return BadRequest("Insufficient balance in sender account.");

            }
            var recipient = await _accountRepository.GetAccountByAccountNumberAsync(model.RecipientAccountNumber);
            if (recipient == null)
            {
                return NotFound("Recipient account not found.");
            }

            if (recipient.Status != "Active")
            {
                return BadRequest("Receiver account must be active to perform transfers.");
            }
            if (recipient.Status == "Freezed")
            {
                return BadRequest("Recipient account is freezed");
            }
            if (recipient.AccountId == sender.AccountId)
            {
                return BadRequest("Sender and recipient accounts cannot be the same.");
            }

            sender.Balance -= model.Amount;
            recipient.Balance += model.Amount;

            await _accountRepository.UpdateAccountAsync(sender);
            await _accountRepository.UpdateAccountAsync(recipient);

            var transaction = new Transaction
            {
                AccountId = sender.AccountId,
                RecipientAccountId = recipient.AccountId,
                TransactionType = "Transfer",
                Amount = model.Amount,
                TransactionDate = DateTime.Now.Date,
                Description = model.Description ?? "Transfer transaction",
                Status = "Completed"
            };

            await _transactionRepository.AddTransactionAsync(transaction);

            return Ok(new
            {
                Message = "Transfer successful.",
                SenderBalance = sender.Balance,
                RecipientAccount = recipient.AccountNumber
            });
        }

        #endregion
    }
}
