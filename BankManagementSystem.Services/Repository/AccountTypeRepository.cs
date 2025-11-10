using BankManagementSystem.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services.Repository
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly AppDbContext _context;

        public AccountTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAccountTypeAsync(AccountType accountType)
        {
            _context.AccountTypeSet.Add(accountType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountTypeAsync(int id)
        {
            var accountType = await _context.AccountTypeSet.FindAsync(id);
            _context.AccountTypeSet.Remove(accountType);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AccountType>> GetAllAccountTypesAsync()
        {
            var accountTypeList = await _context.AccountTypeSet.ToListAsync();
            return accountTypeList;
        }

        public async Task<AccountType> GetAccountTypeByIdAsync(int id)
        {
            var accountType = await _context.AccountTypeSet.FindAsync(id);
            return accountType ?? throw new NotImplementedException();
        }

        public async Task UpdateAccountTypeAsync(AccountType accountType)
        {
            _context.AccountTypeSet.Update(accountType);
            await _context.SaveChangesAsync();
        }
    }
}
