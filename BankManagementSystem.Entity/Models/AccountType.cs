using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Models
{
    [Table("AccountType")]
    public class AccountType
    {
        [Key]
        public int AccountTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public String TypeName { get; set; }
        [StringLength(200)]
        public String? Description { get; set; }
        [Required]
        [Range(0.00, 10.00, ErrorMessage = "Interest rate must be between 0 and 10.")]
        [Column(TypeName = "decimal(5,4)")]
        public decimal InterestRate { get; set; }
        [JsonIgnore]
        public ICollection<Account>? Accounts { get; set; }

    }
}
