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
            try
            {
                _context.AccountSet.Add(account);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteAccountAsync(int id)
        {
            try
            {
                var account = await _context.AccountSet.FindAsync(id);
                _context.AccountSet.Remove(account);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<AccountResponse>> GetAllAccountsAsync()
        {
            try
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<Account> GetAccountByIdAsync(int id)
        {
            try
            {
                var account = await _context.AccountSet.FindAsync(id);
                return account ?? throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateAccountAsync(Account account)
        {
            try
            {
                _context.AccountSet.Update(account);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _context.AccountSet
                        .Where(a => a.CustomerId == customerId)
                        .Include(a => a.AccountTypeSet)
                        .OrderByDescending(a => a.CreatedDate)
                        .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Account?> GetAccountByAccountNumberAsync(string accountNumber)
        {
            try
            {
                return await _context.AccountSet.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
