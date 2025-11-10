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
            return customer ?? throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetCustomerBySearchAsync(string customerName, string aadhar, string status, string phone)
        {
            var query = from customer in _context.CustomerSet select customer;
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
            var customers = await query.ToListAsync();
            return customers;
        }
        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.CustomerSet.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
