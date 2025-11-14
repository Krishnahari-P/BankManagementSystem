using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Client.Dto
{
    public class AccountRequest
    {
        public int CustomerId { get; set; }
        public int AccountTypeId { get; set; }
        public decimal InitialDeposit { get; set; }
    }

}
