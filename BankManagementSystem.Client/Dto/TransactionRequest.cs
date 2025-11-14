using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Client.Dto
{
    public class TransactionRequest
    {
        public int AccountId { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }

}
