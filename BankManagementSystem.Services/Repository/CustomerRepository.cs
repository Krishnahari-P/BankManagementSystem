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
            try
            {
                _context.CustomerSet.Add(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            try
            {
                var customer = await _context.CustomerSet.FindAsync(id);
                if(customer == null)
                {
                    throw new Exception("Customer not found");
                }
                _context.CustomerSet.Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _context.CustomerSet
                .Include(c => c.ApprovedByUserSet)
                .ToListAsync();
                return customers;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _context.CustomerSet.FindAsync(id);
                if (customer == null)
                    throw new Exception("Customer not found");
                return customer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<Customer?> GetCustomerByUserIdAsync(string userId)
        {
            try
            {
                return await _context.CustomerSet.FirstOrDefaultAsync(c => c.ApplicationUserID == userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<Customer?> GetExistingCustomerAsync(string aadhar, string? pan, string phone)
        {
            try
            {
                return await _context.CustomerSet
                       .FirstOrDefaultAsync(c =>
                           c.AadharNumber == aadhar ||
                           c.PAN == pan ||
                           c.Phone == phone);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Customer>> GetCustomerBySearchAsync(string? customerName, string? aadhar, string? status, string? phone)
        {
            try
            {
                var query = _context.CustomerSet.AsQueryable();
                if (!string.IsNullOrWhiteSpace(customerName))
                {
                    query = query.Where(x =>x.CustomerName!=null && x.CustomerName.Contains(customerName));
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
                    query = query.Where(x =>x.Phone!=null && x.Phone.Contains(phone));
                }
                return await query.Include(c => c.ApprovedByUserSet).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task UpdateCustomerAsync(Customer customer)
        {
            try
            {
                _context.CustomerSet.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
