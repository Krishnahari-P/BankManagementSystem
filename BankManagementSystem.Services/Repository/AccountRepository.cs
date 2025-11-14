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
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAccountAsync(Account account)
        {
            _context.AccountSet.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await _context.AccountSet.FindAsync(id);
            _context.AccountSet.Remove(account);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AccountResponse>> GetAllAccountsAsync()
        {
            return await _context.AccountSet
                .Include(a => a.CustomerSet)
                .Include(a => a.AccountTypeSet)
                .OrderByDescending(a => a.CreatedDate)
                .Select(a => new AccountResponse
                {
                    AccountId = a.AccountId,
                    AccountNumber = a.AccountNumber,
                    CustomerId = a.CustomerId,
                    CustomerName = a.CustomerSet != null ? a.CustomerSet.CustomerName : null,
                    AccountTypeId = a.AccountTypeId,
                    AccountTypeName = a.AccountTypeSet != null ? a.AccountTypeSet.TypeName : null,
                    Balance = a.Balance,
                    CreatedDate = a.CreatedDate,
                    Status = a.Status
                })
                .ToListAsync();
        }


        public async Task<Account> GetAccountByIdAsync(int id)
        {
            var account = await _context.AccountSet.FindAsync(id);
            return account ?? throw new NotImplementedException();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.AccountSet.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
            return await _context.AccountSet
                .Where(a => a.CustomerId == customerId)
                .Include(a => a.AccountTypeSet)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }

        public async Task<Account?> GetAccountByAccountNumberAsync(string accountNumber)
        {
            return await _context.AccountSet.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

    }
}
