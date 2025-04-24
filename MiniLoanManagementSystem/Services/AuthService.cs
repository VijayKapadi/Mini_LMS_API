using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MiniLoanManagementSystem.Helpers;
using MiniLoanManagementSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniLoanManagementSystem.Services
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }

    public class AuthService : IAuthService
    {
        private readonly JWTSettings _settings;

        public AuthService(IOptions<JWTSettings> options) => _settings = options.Value;

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _settings.JwtTokenIssuer,
                audience: _settings.JwtTokenAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.JwtTokenExpires),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
