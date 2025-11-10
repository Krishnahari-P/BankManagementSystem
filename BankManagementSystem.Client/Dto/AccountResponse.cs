using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankManagementSystem.Client.Dto
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public String AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public int AccountTypeId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public String Status { get; set; }
    }
}
