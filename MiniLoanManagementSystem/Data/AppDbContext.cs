using Microsoft.EntityFrameworkCore;
using MiniLoanManagementSystem.DTOs;
using MiniLoanManagementSystem.Models;

namespace MiniLoanManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<LoanapplicationDtos> LoanApplications { get; set; }
    }
   
}


