namespace MiniLoanManagementSystem.DTOs
{
    public class LoginRequests
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
   
    public class RegisterRequest(string Username, string Password, string Role);
}
