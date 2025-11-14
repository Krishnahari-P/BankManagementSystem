using BankManagementSystem.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services.Repository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task<List<Customer>> GetCustomerBySearchAsync(string? customerName, string? aadhar, string? status, string? phone);
        Task<Customer?> GetExistingCustomerAsync(string aadhar, string? pan, string phone);
        Task<Customer?> GetCustomerByUserIdAsync(string userId);

    }
}
