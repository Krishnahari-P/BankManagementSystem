using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Client.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password Confirmation failed due to mismatch")]
        public string? ConfirmPassword { get; set; }
    }
}
