using MiniLoanManagementSystem.Models;

namespace MiniLoanManagementSystem.DTOs
{
    public class RepaymentScheduleDtos
    {
        public int Id { get; set; }       
        public Int32 Tenure { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal TotalPayment { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
    }   
   
}
