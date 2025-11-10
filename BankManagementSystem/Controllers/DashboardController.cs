using BankManagementSystem.Entity.Dto;
using BankManagementSystem.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("Dashboard")]
        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalCustomers = _context.CustomerSet.Count(),
                TotalAccounts = _context.AccountSet.Count(),
                TotalAccountTypes = _context.AccountTypeSet.Count(),
                TotalEmployees = _context.EmployeeSet.Count(),
                TotalTransactions = _context.TransactionSet.Count(),
            };
            return Ok(model);
        }
    }
}
