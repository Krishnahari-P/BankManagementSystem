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
            try
            {
                _context.AccountTypeSet.Add(accountType);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteAccountTypeAsync(int id)
        {
            try
            {
                var accountType = await _context.AccountTypeSet.FindAsync(id);
                _context.AccountTypeSet.Remove(accountType);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<AccountType>> GetAllAccountTypesAsync()
        {
            try
            {
                var accountTypeList = await _context.AccountTypeSet.ToListAsync();
                return accountTypeList;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }
        }

        public async Task<AccountType> GetAccountTypeByIdAsync(int id)
        {
            try
            {
                var accountType = await _context.AccountTypeSet.FindAsync(id);
                return accountType ?? throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateAccountTypeAsync(AccountType accountType)
        {
            try
            {
                _context.AccountTypeSet.Update(accountType);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
