using BankManagementSystem.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services.Repository
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(int id);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int id);
    }
}
