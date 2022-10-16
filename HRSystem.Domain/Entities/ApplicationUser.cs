using HRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public Employee Employee { get; private set; }

        public string CreateToken(IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims =
            {
                new Claim("userId", Id.ToString()),
                new Claim("email", Email),
                new Claim("name", UserName)
            };

            DateTime tokenExpirationTime = DateTime.UtcNow.AddMinutes(double.Parse(configuration["Jwt:TokenExpirationInMinutes"]));

            var token = new JwtSecurityToken(
               configuration["Jwt:Issuer"],
                configuration["Jwt:linkDev.com"],
                claims,
                null,
                tokenExpirationTime,
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
