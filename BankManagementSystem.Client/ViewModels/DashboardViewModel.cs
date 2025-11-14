using BankManagementSystem.Client.Dto;

namespace BankManagementSystem.Client.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalAccounts { get; set; }
        public int TotalAccountTypes { get; set; }
        public int TotalTransactions { get; set; }
        public List<TransactionResponse>? Transactions { get; set; }
    }
}
