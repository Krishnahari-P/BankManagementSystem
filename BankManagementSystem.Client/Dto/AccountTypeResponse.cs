using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankManagementSystem.Client.Dto
{
    public class AccountTypeResponse
    {
        public int AccountTypeId { get; set; }
        public String TypeName { get; set; }
        public String? Description { get; set; }
        public decimal InterestRate { get; set; }
    }
}
