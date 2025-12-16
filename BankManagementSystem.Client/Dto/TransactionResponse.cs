using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankManagementSystem.Client.Dto
{
    public class TransactionResponse
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string? AccountNumber { get; set; }
        public string? TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string? Status { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public string? SenderAccountNumber { get; set; }
        public string? SenderName { get; set; }
        public int? RecipientAccountId { get; set; }
        public string? RecipientAccountNumber { get; set; }
        public string? RecipientName { get; set; }
        public string? ProcessedByUserId { get; set; }
    }

}
