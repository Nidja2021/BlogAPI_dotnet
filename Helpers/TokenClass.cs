using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Helpers
{
    public class TokenClass
    {
        private readonly IConfiguration _configuration;

        public TokenClass(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(User user) {

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.roles.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:SecretKey").Value!
            ));

            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature
            );

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                issuer: _configuration.GetSection("Jwt:Issuer").Value!,
                audience: _configuration.GetSection("Jwt:Audience").Value!,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}