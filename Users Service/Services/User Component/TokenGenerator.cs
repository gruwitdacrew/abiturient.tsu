using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users_Service.Models;

namespace Users_Service.Services
{
    public class TokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        public TokenGenerator(AuthenticationConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(User user, List<string> userRoles)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.AccessTokenSecret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            JwtSecurityToken token = new JwtSecurityToken(
                _configuration.Issuer,
                _configuration.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpirationMinutes),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
