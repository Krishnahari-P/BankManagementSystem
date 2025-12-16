using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Client.Dto
{
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public string? ApplicationUserID { get; set; }
        public String? StaffCode { get; set; }
        public String? EmployeeName { get; set; }
        public String? Phone { get; set; }
        public String? JobTitle { get; set; }
        public DateTime HiredDate { get; set; }
        public string? Status { get; set; }
    }
}
