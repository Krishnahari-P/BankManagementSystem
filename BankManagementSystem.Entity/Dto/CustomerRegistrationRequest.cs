using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Dto
{
    public class CustomerRegistrationRequest
    {
        [Required]
        [StringLength(100)]
        public string? CustomerName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string? Occupation { get; set; }

        [Required]
        [StringLength(10)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(12)]
        public string? AadharNumber { get; set; }

        [StringLength(10)]
        public string? PAN { get; set; }
    }

}
