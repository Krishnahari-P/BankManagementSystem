using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Dto
{
    public class DashboardViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalAccounts { get; set; }
        public int TotalAccountTypes { get; set; }
        public int TotalTransactions { get; set; }
    }
}
