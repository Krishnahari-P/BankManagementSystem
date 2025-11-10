namespace BankManagementSystem.Client.Dto
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }
        public string? ApplicationUserID { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public string? Phone { get; set; }
        public String? ApprovedByUserId { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? AadharNumber { get; set; }
        public string? PAN { get; set; }
        public string? CustomerImageURL { get; set; }
        public string Status { get; set; }
    }
}
