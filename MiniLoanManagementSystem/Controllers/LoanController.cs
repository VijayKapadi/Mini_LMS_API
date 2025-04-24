using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniLoanManagementSystem.DTOs;
using MiniLoanManagementSystem.Models;
using MiniLoanManagementSystem.Repositories;
using MiniLoanManagementSystem.Repositories.LoanRepository;
using MiniLoanManagementSystem.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MiniLoanManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository _loanRepo;
        private readonly IUserRepository _userRepo;


        public LoanController(ILoanRepository loanRepo, IUserRepository userRepo)
        {
            _loanRepo = loanRepo;
            _userRepo = userRepo;
        }

       
        [HttpPost("ApplyForLoan")]
        public async Task<IActionResult> ApplyForLoan([FromBody] LoanapplicationDtos request)
        {            
            var username = request.Username;
            var user = await _userRepo.GetByUsernameAsync(username!);
            if (user is null) return Unauthorized();

            var loan = new LoanapplicationDtos
            {
                Username = username,
                LoanAmount = request.LoanAmount,
                LoanTermMonths = request.LoanTermMonths,
                LoanPurpose = request.LoanPurpose, 
                Status = request.Status
            };

            await _loanRepo.AddLoanAsync(loan);
            return Ok(new { Message = "Loan application submitted successfully.", LoanId = loan.Id });
        }
        
       [HttpPost("UpdateBorrowerLoans")]
        public async Task<IActionResult> GetBorrowerLoans([FromBody] LoanapplicationDtos dto)
        {

            var username = dto.Username;
            var user = await _userRepo.GetByUsernameAsync(username!);
            if (user is null) return Unauthorized();

          var  response =  await _loanRepo.UpdateLoanAsync(dto, user.Role);
            if (response == null) return NotFound("Loan not found");
            return Ok(new { Message = "Loan application updated successfully.", response });
           // return Ok(response);
        }

       [HttpGet("GetLoans/{username}")]
        public async Task<IActionResult> GetLoans(string username)
        {
            
           // var username = UserName;
            var user = await _userRepo.GetByUsernameAsync(username!);
            if (user is null) return Unauthorized();

            var response = await _loanRepo.GetLoanAsync(username, user.Role);
            if (response == null) return NotFound("Loan not found");
            // return Ok(new { Message = "Loan application submitted.", LoanId = loan.Id });
            return Ok(response);
        }

        [HttpPost("AprroveRejectLoans")]
        public async Task<IActionResult> AprroveRejectLoans([FromBody] LoanapplicationDtos dto)
        {
            var username = dto.Username;
            var user = await _userRepo.GetByUsernameAsync(username!);
            if (user is null) return Unauthorized();

            var updated = await _loanRepo.AprroveRejectLoans(dto, user.Role);
            if (updated == null) return NotFound("Loan not found");
          
            return Ok(new { Message = "Loan application " + updated.Status + " successfully.", updated });
        }

        [HttpPost("RepaymentSchedule")]
        public async Task<IActionResult> RepaymentSchedule([FromBody] RepaymentScheduleDtos dto)
        {
            

            var updated = await _loanRepo.RepaymentSchedule(dto);
            if (updated == null) return NotFound("Loan not found");
        
            return Ok(updated);
        }
    }
}
