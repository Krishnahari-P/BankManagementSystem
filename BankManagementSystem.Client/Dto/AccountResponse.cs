using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankManagementSystem.Client.Dto
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public String? AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int AccountTypeId { get; set; }
        public string? AccountTypeName { get; set; }
        public decimal Balance { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public String? Status { get; set; }
        public AccountTypeResponse? AccountTypeSet { get; set; }
        public List<SelectListItem>? AccountTypeList { get; set; }

    }
}
