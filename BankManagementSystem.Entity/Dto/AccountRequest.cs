using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Dto
{
    public class AccountRequest
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int AccountTypeId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Initial deposit must be non-negative.")]
        public decimal InitialDeposit { get; set; }
    }

}
