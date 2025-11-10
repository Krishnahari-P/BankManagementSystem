using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Client.Dto
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Dob { get; set; }
        public int? Age { get; set; }
    }
}
