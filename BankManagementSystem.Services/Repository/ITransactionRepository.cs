using BankManagementSystem.Entity.Dto;
using BankManagementSystem.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services.Repository
{
    public interface ITransactionRepository
    {
        Task<List<TransactionResponse>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task AddTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
        Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
        Task<List<Transaction>> GetPendingTransactionsAsync();
        Task<List<Transaction>> GetTransactionBySearchAsync(string? status, DateTime? date);
        Task<List<TransactionResponse>> GetTransactionsByCustomerIdAsync(int customerId);

    }
}
