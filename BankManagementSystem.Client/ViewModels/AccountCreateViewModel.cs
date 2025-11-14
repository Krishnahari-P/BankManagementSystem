using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankManagementSystem.Client.ViewModels
{
    public class AccountCreateViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please select an account type.")]
        public int AccountTypeId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Initial deposit must be non-negative.")]
        public decimal InitialDeposit { get; set; }

        public List<SelectListItem>? AccountTypes { get; set; }
    }
}
