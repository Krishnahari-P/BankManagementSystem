using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Client.Dto
{
    public class TransferRequest
    {
        [Required]
        public int SenderAccountId { get; set; }

        [Required(ErrorMessage = "Recipient account number is required.")]
        public string RecipientAccountNumber { get; set; } = string.Empty;

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Transfer amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
    }
}
