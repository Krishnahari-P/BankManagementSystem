using BankManagementSystem.Entity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Security
{
    public class ApplicationUser : IdentityUser
    {
        public string? Status { get; set; }
        public bool IsActive { get; set; }
        public Customer CustomerSet { get; set; }
        public Employee EmployeeSet { get; set; }
        public ICollection<Customer> ApprovedCustomers { get; set; }
        public ICollection<Transaction> ProcessedTransactions { get; set; }
    }
}
