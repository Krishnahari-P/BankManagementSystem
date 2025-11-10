using BankManagementSystem.Entity.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Models
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        [StringLength(20)]
        public String TransactionType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public String? Status { get; set; }
        public DateTime TransactionDate { get; set; }
        [StringLength(200)]
        public String? Description { get; set; }
        public int? RecipientAccountId { get; set; }
        [StringLength(450)]
        public String? ProcessedByUserId { get; set; }
        public ApplicationUser? ProcessedByUserSet { get; set; }
        public Account? AccountSet { get; set; }
        public Account? RecipientAccountSet { get; set; }
    }
}
