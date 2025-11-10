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
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [StringLength(450)]
        public string? ApplicationUserID { get; set; }
        [Required]
        [StringLength(20)]
        public String StaffCode { get; set; }
        [Required]
        [StringLength(100)]
        public String EmployeeName { get; set; }
        [Required]
        [StringLength(10)]
        public String Phone {  get; set; }
        [Required]
        [StringLength(50)]
        public String JobTitle {  get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime HiredDate { get; set; }
        public ApplicationUser? ApplicationUserSet { get; set; }
    }
}
