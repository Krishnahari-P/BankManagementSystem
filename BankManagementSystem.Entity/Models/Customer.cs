using BankManagementSystem.Entity.Security;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [StringLength(450)]
        public string? ApplicationUserID { get; set; }
        [Required]
        [StringLength(100)]
        public String? CustomerName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [StringLength(50)]
        public String? Occupation {  get; set; }
        [StringLength(10)]
        [Required]
        public String? Phone {  get; set; }
        public String? ApprovedByUserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ApprovalDate {  get; set; }
        [StringLength(12)]
        [Required]
        public String AadharNumber { get; set; }
        [StringLength(10)]
        public String? PAN {  get; set; }
        public String? CustomerImageURL { get; set; }
        public string Status { get; set; } = "Pending";
        public ApplicationUser? ApplicationUserSet { get; set; } 
        public ApplicationUser? ApprovedByUserSet { get; set; } 
        public ICollection<Account>? Accounts { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
