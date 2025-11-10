using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Client.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password
        {
            get; set;
        }
    }
}
