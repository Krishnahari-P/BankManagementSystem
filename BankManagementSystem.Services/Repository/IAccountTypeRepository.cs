using BankManagementSystem.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services.Repository
{
    public interface IAccountTypeRepository
    {
        Task<List<AccountType>> GetAllAccountTypesAsync();
        Task<AccountType> GetAccountTypeByIdAsync(int id);
        Task AddAccountTypeAsync(AccountType accountType);
        Task UpdateAccountTypeAsync(AccountType accountType);
        Task DeleteAccountTypeAsync(int id);
    }
}
