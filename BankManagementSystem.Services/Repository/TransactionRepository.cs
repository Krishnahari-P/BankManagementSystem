using BankManagementSystem.Entity.Dto;
using BankManagementSystem.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.TransactionSet.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.TransactionSet.FindAsync(id);
            _context.TransactionSet.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TransactionResponse>> GetAllTransactionsAsync()
        {
            return await _context.TransactionSet
                .Include(t => t.AccountSet)
                .Include(t => t.RecipientAccountSet)
                .OrderByDescending(t => t.TransactionDate)
                .Select(t => new TransactionResponse
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    AccountNumber = t.AccountSet.AccountNumber,    
                    TransactionType = t.TransactionType,
                    Amount = t.Amount,
                    Status = t.Status,
                    TransactionDate = t.TransactionDate,
                    Description = t.Description,
                    RecipientAccountId = t.RecipientAccountId,
                    RecipientAccountNumber = t.RecipientAccountSet != null
                        ? t.RecipientAccountSet.AccountNumber
                        : null,                                  
                    ProcessedByUserId = t.ProcessedByUserId
                })
                .ToListAsync();
        }


        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.TransactionSet.FindAsync(id);
            return transaction ?? throw new NotImplementedException();
        }

        public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _context.TransactionSet
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
        }

        public async Task<List<Transaction>> GetPendingTransactionsAsync()
        {
            return await _context.TransactionSet
                .Where(t => t.Status == "Pending")
                .Include(t => t.AccountSet)
                .OrderBy(t => t.TransactionDate)
                .ToListAsync();
        }
        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            _context.TransactionSet.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetTransactionBySearchAsync(string? status, DateTime? date)
        {
            var query = _context.TransactionSet.AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(x => x.Status.Contains(status));
            }
            if (date != null)
            {
                query = query.Where(x => x.TransactionDate.Date == date.Value.Date);
            }
            return await query.ToListAsync();
        }

        public async Task<List<TransactionResponse>> GetTransactionsByCustomerIdAsync(int customerId)
        {
            var accountIds = await _context.AccountSet
                .Where(a => a.CustomerId == customerId)
                .Select(a => a.AccountId)
                .ToListAsync();

            var transactions = await _context.TransactionSet
                .Include(t => t.AccountSet)
                .Where(t => accountIds.Contains(t.AccountId))
                .Select(t => new TransactionResponse
                {
                    TransactionId = t.TransactionId,
                    TransactionType = t.TransactionType,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    Description = t.Description,
                    Status = t.Status,
                    AccountNumber = t.AccountSet != null ? t.AccountSet.AccountNumber : null
                })
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            return transactions;
        }
    }
}
