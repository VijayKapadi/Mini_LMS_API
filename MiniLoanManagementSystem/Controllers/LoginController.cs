using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MiniLoanManagementSystem.DTOs;
using MiniLoanManagementSystem.Repositories;
using MiniLoanManagementSystem.Services;

namespace MiniLoanManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IAuthService _authService;

        public LoginController(IUserRepository userRepo, IAuthService authService)
        {
            _userRepo = userRepo;
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequests request)
        {
            var user = await _userRepo.GetByUsernameAsync(request.Username);
            if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _authService.GenerateToken(user);
            return Ok(new { Token = token, UserName = user.Username, Role = user.Role  });
        }
    }
}
