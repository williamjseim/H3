using HackGame.Api.Data;
using HackGame.Api.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace HackGame.Api.TokenAuthorization
{
    public class JwtAuthorization
    {
        private readonly IConfiguration _config;
        private readonly HackerGameDbContext _db;
        public JwtAuthorization(IConfiguration config, HackerGameDbContext db)
        {
            _config = config;
            _db = db;
        }

        public string GenerateJsonWebToken(string username, string password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("username", username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", "Admin"),
            };

            var token = new JwtSecurityToken(_config["JwtSettings:Issuer"],
                _config["JwtSettings:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
