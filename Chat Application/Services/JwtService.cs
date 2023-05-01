using Chat_Application.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Chat_Application.Services
{
    public interface IJwtService
    {
        string CreateToken(User user);
    }
    public class JwtService : IJwtService
    {

        private readonly IConfiguration _config;
        public JwtService(IConfiguration configuration) {
            _config = configuration;
        }
        public string CreateToken(User request)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Sid, Convert.ToString(request.UserId))
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
