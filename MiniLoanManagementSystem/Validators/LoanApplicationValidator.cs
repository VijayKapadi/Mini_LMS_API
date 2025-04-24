using FluentValidation;
using MiniLoanManagementSystem.DTOs;

namespace MiniLoanManagementSystem.Validators
{
    public class LoanApplicationValidator : AbstractValidator<LoanapplicationDtos>
    {
        public LoanApplicationValidator()
        {
            RuleFor(x => x.LoanAmount)
                .GreaterThan(0).WithMessage("Loan amount must be greater than 0.");

            RuleFor(x => x.LoanTermMonths)
                .InclusiveBetween(1, 360).WithMessage("Term must be between 1 and 360 months.");

            RuleFor(x => x.LoanPurpose)
            .NotEmpty().WithMessage("Loan purpose is required");

            RuleFor(x => x.Username)
           .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Status)
            .Must(status => status == "Pending" || status == "Approved" || status == "Rejected")
            .WithMessage("Status must be 'Pending', 'Approved', or 'Rejected'");
        }
    }
}
