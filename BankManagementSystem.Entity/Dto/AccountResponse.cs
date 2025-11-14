using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Dto
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public string? AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }     
        public int AccountTypeId { get; set; }
        public string? AccountTypeName { get; set; }  
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Status { get; set; }
    }

}
