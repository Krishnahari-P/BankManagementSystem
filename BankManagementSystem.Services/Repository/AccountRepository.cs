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

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            var accountList = await _context.AccountSet.ToListAsync();
            return accountList;
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
    }
}
