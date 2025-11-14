using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Client.Dto
{
    public class EmployeeRequest
    {
        [Required(ErrorMessage ="Employee name is required")]
        [StringLength(100)]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage ="Phone number is required")]
        [StringLength(10)]
        public string Phone { get; set; }

        [Required(ErrorMessage ="Job title is required")]
        [StringLength(50)]
        public string JobTitle { get; set; }
    }
}
