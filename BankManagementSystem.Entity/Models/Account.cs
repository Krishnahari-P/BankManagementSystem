using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        [StringLength(20)]
        public String AccountNumber { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int AccountTypeId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0;
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(10)]
        public String Status { get; set; } = "Pending";
        public Customer? CustomerSet { get; set; }
        public AccountType? AccountTypeSet { get; set; }
        public ICollection<Transaction>? InitiatedTransactions { get; set; }
        public ICollection<Transaction>? ReceivedTransactions { get; set; }
    }
}
