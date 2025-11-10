using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Client.ViewModels
{
    public class RegisterViewModel
    {
        [EmailAddress]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password doesnot match")]
        public string ConfirmPassword { get; set; }
    }
}
