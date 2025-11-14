namespace BankManagementSystem.Client.Dto
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public int ExpiresIn { get; set; }
        public string? CustomerId { get; set; }

    }

}
