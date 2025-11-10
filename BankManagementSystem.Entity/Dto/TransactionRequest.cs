using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Dto
{
    public class TransactionRequest
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
    }

}
