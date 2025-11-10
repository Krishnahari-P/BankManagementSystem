using BankManagementSystem.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            _context.EmployeeSet.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.EmployeeSet.FindAsync(id);
            _context.EmployeeSet.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employeeList = await _context.EmployeeSet.ToListAsync();
            return employeeList;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.EmployeeSet.FindAsync(id);
            return employee ?? throw new NotImplementedException();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.EmployeeSet.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
