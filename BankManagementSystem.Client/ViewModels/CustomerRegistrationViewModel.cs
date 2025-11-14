using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Client.ViewModels
{
    public class CustomerRegistrationViewModel
    {
        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string? CustomerName { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(50)]
        public string? Occupation { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must contain only digits")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Aadhar number is required")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Aadhar number must be exactly 12 digits")]
        public string? AadharNumber { get; set; }
        [StringLength(10, ErrorMessage = "PAN must be exactly 10 characters")]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN format")]
        public string? PAN { get; set; }
    }
}
