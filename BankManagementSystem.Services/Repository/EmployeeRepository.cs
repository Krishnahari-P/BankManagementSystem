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
            try
            {
                _context.EmployeeSet.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                var employee = await _context.EmployeeSet.FindAsync(id);
                _context.EmployeeSet.Remove(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                var employeeList = await _context.EmployeeSet.ToListAsync();
                return employeeList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public async Task<List<Employee>> GetAllEmployeesBySearchAsync(string? name, string? phone)
        {
            try
            {
                var query = _context.EmployeeSet.AsQueryable();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(x => x.EmployeeName.Contains(name));
                }
                if (!string.IsNullOrWhiteSpace(phone))
                {
                    query = query.Where(x => x.Phone.Contains(phone));
                }
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _context.EmployeeSet.FindAsync(id);
                return employee ?? throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                _context.EmployeeSet.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }
    }
}
