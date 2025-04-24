using MiniLoanManagementSystem.DTOs;
using MiniLoanManagementSystem.Models;

namespace MiniLoanManagementSystem.Repositories.LoanRepository
{
    public interface ILoanRepository
    {
        Task AddLoanAsync(LoanapplicationDtos loan);
        Task<LoanapplicationDtos> UpdateLoanAsync(LoanapplicationDtos dto, string Role);
        Task<IEnumerable<LoanapplicationDtos>> GetLoanAsync(string username, string Role);

        Task<LoanapplicationDtos> AprroveRejectLoans(LoanapplicationDtos dto, string Role);
        Task<List<RepaymentScheduleDtos>> RepaymentSchedule(RepaymentScheduleDtos dto);
    }
}
