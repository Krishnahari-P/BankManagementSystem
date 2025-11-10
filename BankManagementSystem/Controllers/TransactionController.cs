using BankManagementSystem.Entity.Dto;
using BankManagementSystem.Entity.Models;
using BankManagementSystem.Services.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
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
                Status = "Pending"
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

        #region GetTransactionByAccount
        [HttpGet("GetTransactionsByAccount")]
        public async Task<IActionResult> GetTransactionsByAccount(int accountId)
        {
            var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
            if (transactions == null || !transactions.Any())
                return NotFound("No transactions found for this account.");

            return Ok(transactions);
        }
        #endregion
    }
}
