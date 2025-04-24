namespace MiniLoanManagementSystem.Models
{
    public class RepaymentSchedule
    {
        public int Id { get; set; }
        public LoanApplication LoanApplication { get; set; } = null!;
        public int Tenure { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal TotalPayment { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
    }
}
