namespace MiniLoanManagementSystem.DTOs
{
    public class LoanapplicationDtos
    {
        public int Id { get; set; }
        public decimal LoanAmount { get; set; }
        public int LoanTermMonths { get; set; }
        public string? LoanPurpose { get; set; } = string.Empty;
        public string? Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional: Link to a user (foreign key)
        public string? Username { get; set; }
    }   
   
}
