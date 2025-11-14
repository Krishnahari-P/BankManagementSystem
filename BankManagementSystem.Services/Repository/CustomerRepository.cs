using BankManagementSystem.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.CustomerSet.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.CustomerSet.FindAsync(id);
            _context.CustomerSet.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customerList = await _context.CustomerSet.ToListAsync();         
            return customerList;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.CustomerSet.FindAsync(id);
            //return customer ?? throw new NotImplementedException();
            if (customer == null)
                return null;
            return customer;

        }

        public async Task<Customer?> GetCustomerByUserIdAsync(string userId)
        {
            return await _context.CustomerSet.FirstOrDefaultAsync(c => c.ApplicationUserID == userId);
        }


        public async Task<Customer?> GetExistingCustomerAsync(string aadhar, string? pan, string phone)
        {
            return await _context.CustomerSet
                .FirstOrDefaultAsync(c =>
                    c.AadharNumber == aadhar ||
                    c.PAN == pan ||
                    c.Phone == phone);
        }

        public async Task<List<Customer>> GetCustomerBySearchAsync(string? customerName, string? aadhar, string? status, string? phone)
        {
            var query = _context.CustomerSet.AsQueryable();
            if (!string.IsNullOrWhiteSpace(customerName))
            {
                query = query.Where(x => x.CustomerName.Contains(customerName));
            }
            if (!string.IsNullOrWhiteSpace(aadhar))
            {
                query = query.Where(x => x.AadharNumber.Contains(aadhar));
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(x => x.Status.Contains(status));
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query.Where(x => x.Phone.Contains(phone));
            }
            return await query.ToListAsync();
        }
        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.CustomerSet.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
