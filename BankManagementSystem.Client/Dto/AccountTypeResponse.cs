using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace BankManagementSystem.Client.Dto
{
    public class AccountTypeResponse
    {
        public int AccountTypeId { get; set; }
        public String TypeName { get; set; }
        public String? Description { get; set; }
        [Range(0.00, 10.00, ErrorMessage = "Interest rate must be between 0 and 10.")]
        public decimal InterestRate { get; set; }

    }
}
