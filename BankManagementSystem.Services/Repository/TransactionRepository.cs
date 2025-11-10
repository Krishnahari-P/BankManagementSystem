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

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            var transactionList = await _context.TransactionSet.ToListAsync();
            return transactionList;
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
    }
}
