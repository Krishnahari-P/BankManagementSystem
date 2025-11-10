using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankManagementSystem.Client.Dto
{
    public class TransactionResponse
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public String TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public String? Description { get; set; }
        public int? RecipientAccountId { get; set; }
        public String? ProcessedByUserId { get; set; }
    }
}
