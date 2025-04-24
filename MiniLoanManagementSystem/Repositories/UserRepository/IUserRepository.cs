using MiniLoanManagementSystem.Models;

namespace MiniLoanManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddUserAsync(User user);
    }
}
