namespace MiniLoanManagementSystem.Models
{
    public class LoanApplication
    {
        public int Id { get; set; }
        public decimal LoanAmount { get; set; }
        public int LoanTermMonths { get; set; }
        public string? LoanPurpose { get; set; } = string.Empty;
        public string? Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public string? Username { get; set; }      

    }
}
