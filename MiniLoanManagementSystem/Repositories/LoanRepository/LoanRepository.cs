using System.Data;
using Microsoft.EntityFrameworkCore;
using MiniLoanManagementSystem.Data;
using MiniLoanManagementSystem.DTOs;
using MiniLoanManagementSystem.Models;

namespace MiniLoanManagementSystem.Repositories.LoanRepository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;
        public LoanRepository(AppDbContext context) => _context = context;

        public async Task AddLoanAsync(LoanapplicationDtos loan)
        {
            _context.LoanApplications.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<LoanapplicationDtos> UpdateLoanAsync(LoanapplicationDtos dto, string Role)
        {
            var loan = await _context.LoanApplications
                 .FirstOrDefaultAsync(l => l.Username == dto.Username && l.Id == dto.Id);

            if (loan == null) return null; ;
            if (Role != "Admin")
            {
                loan.Id = dto.Id;
                loan.LoanAmount = dto.LoanAmount;
                loan.LoanTermMonths = dto.LoanTermMonths;
                loan.LoanPurpose = dto.LoanPurpose;
                loan.Status = dto.Status;

                await _context.SaveChangesAsync();

                return loan;
            }
            else return null;

        }

        public async Task<IEnumerable<LoanapplicationDtos>> GetLoanAsync(string username, string Role)
        {
            //return Role == "Admin" ? await _context.LoanApplications.ToListAsync()
            return Role == "Admin" ? await _context.LoanApplications.Where(t => t.Status == "Pending").ToListAsync()

             : await _context.LoanApplications.Where(t => t.Username == username).ToListAsync();


        }

        public async Task<LoanapplicationDtos> AprroveRejectLoans(LoanapplicationDtos dto, string Role)
        {
            var loan = await _context.LoanApplications
                .FirstOrDefaultAsync(l =>  l.Id == dto.Id);

            if (loan == null) return null; ;
            if (Role == "Admin")
            {
                loan.Id = dto.Id;
                loan.LoanAmount = dto.LoanAmount;
                loan.LoanTermMonths = dto.LoanTermMonths;
                loan.LoanPurpose = dto.LoanPurpose;
                loan.Status = dto.Status;

                await _context.SaveChangesAsync();

                return loan;
            }
            else return null;

        }

        public async Task<List<RepaymentScheduleDtos>> RepaymentSchedule(RepaymentScheduleDtos dto)
        {
            var EmiList = new List<RepaymentScheduleDtos>();
            var P = dto.Principal;
            var R = (decimal)(dto.Interest / 12 / 100);
            var N = dto.Tenure;
            var emi = P * R * (decimal)Math.Pow(1 + (double)R, N) / (decimal)(Math.Pow(1 + (double)R, N) - 1);

            var currentDate = DateTime.UtcNow;
            for (int i = 1; i <= N; i++)
            {
                var interest = P * R;
                var principal = emi - interest;
                P -= principal;

                EmiList.Add(new RepaymentScheduleDtos
                {
                    Tenure = i,
                    DueDate = currentDate.AddMonths(i),
                    Principal = Math.Round(principal, 2),
                    Interest = Math.Round(interest, 2),
                    TotalPayment = Math.Round(emi, 2),
                    IsPaid = false
                });
            }

       
            return EmiList;

        }

    }
}
