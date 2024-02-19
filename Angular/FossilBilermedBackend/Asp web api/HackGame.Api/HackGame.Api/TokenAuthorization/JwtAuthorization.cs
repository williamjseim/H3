using HackGame.Api.Data;
using HackGame.Api.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HackGame.Api.TokenAuthorization
{
    public class JwtAuthorization
    {
        const string privateKey = "MIIEpAIBAAKCAQEAil1vXyufPV+l80Xo6ZxvOkafYwV/uBD929N+0dzKaOBNydf/wdU0AmanlMoqF2Icfj/iNLY79Kq7q1CC0wTOCkAlFv0G/ppPoArbj4csNfRh/RC4gksVjM/olhdFMm2tiKndzQCLJzxmfbhzBZkmrhnwRJFIFUl3COBQvE+W7R5exozeSxRHHMvB61/lpngkjs+TmFyeMi3GgcDLJVtYS4fecthbGDEZCaAHDBQG5Q4ervNzyssgo7rGOh8GDZmUm2zUxXmhx3WhS43OjEC2MXx93ris6UrwExzKWoc2Ljvvfvrx6RBg1s4331QZcDtdwbuG0m+2VEGMI9mZohhPZQIDAQABAoIBAEpvs6+2bwd8gnGaxY+P+gKW5b5GwgwLEBcH860BI99B9HdG/a+QfFdiVHtmOzizLnL4+T+0XhLlGusx9/+AETxQf+ObIf0slPObg7z2UmGAZIrZSPnHXQORAiZkxHQ5rubbw5g616G3C8hMx4xWdQmqqKiqo+XXzPL4glacVqyfJCgM8b4UCJlyVBK8Ymqh1eSAmMU8odLHdkqABkOC/74gFRSNdFwnUg8L/qVpl2X90qTE5AjRnNl3nZ8TNespXw49Jpn9n638ytAYMI/EjPMIQ0XdEyJaOtMHSvhJZ/mMcZovRuMd7Naa6HjS2thUUF/LWwKWKIXc8M3bGn34teECgYEAyDX+Grvdv4NUHIQAI+VmzjedNnU3GelWMFpkGVA6deSkbmw/W7+U6J4VsgkQLAWwfKh137EE98qCcK7VrNAc2yNFZrYzdXR9dOB5sA/6toUZcObgHOf+\r\nBD2DScPS56AityCJPx8ChEWNLwPxBHSlbIgVBgmKsAi+AJ2MzhfDmT0CgYEAsOus\r\nwq5BWdKl5Qu1T87ClLtOs479pXT45GBfeFYWsCmQRNrNh/xkD3Amk2q26W8NLUAJf7MQHW0qi4CqOXiCb/KWRRAuGvzbsK9WxSAADkzNTjMkzX7Au38IksfM3mq/w5JYrw/A6QBoJEErP10bTKWz7hWkUGSclVBaBaRf4UkCgYEAmKrDRzN2VjEYlF37lWO6TwmNchdTmwiY1mo25i6NRZTB5gnZDmc6b18MgCP1FvyHpab3q0la1nCvoqlO+mX/ewKcS0QoXSok+FirshudPEymJ7eFscUdp7IYSMlwK3uqvSWsQlutGZvHmtBQmusvsWDbVy0zeiRQbju1QPpSBqkCgYASaTQdPk0Gr+kboNBJcdbF64gRH3w9z6JnAbBtsWfDBVBCGhLxZ85x3NXm2WAXgrr4ToWuiE/RAB9BTS5ptKS+SZxhq4FgxZeHF3gMI3xeAJgM3z2hNOORn3Kg87HaI3m5PG2GKjmFBcnQnAeIHIROwZ+r0wy6YP36e9Yi\r\naCyiIQKBgQC3EL2cfH4B/bSF1a60JNa0JnTE59qLKoIzLnrkXhZMGP3UrsjdoHsYQdl8aS0I1u7u5xiRlkBwiF7q+2YJuVsIUtml4VMTaKs83M0QoXTipn/7z1YD9Q4xFPQFezm+73NUPmr5dYtdQd6mTlPSWe1hm9diQc+emVl3ShUH6gdlIQ==";
        private readonly IConfiguration _config;
        private readonly HackerGameDbContext _db;
        public JwtAuthorization(IConfiguration config, HackerGameDbContext db)
        {
            _config = config;
            _db = db;
        }

        //roles should be verified by check a database but i cant be bothered
        public string GenerateJsonWebToken(string username, string password, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("username", username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", role),
            };

            var token = new JwtSecurityToken(_config["JwtSettings:Issuer"],
                _config["JwtSettings:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);
            var converter = new ASCIIEncoding();
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            byte[] plaintext = converter.GetBytes(jwt);
            var rsa = RSA.Create(2048);
            rsa.ImportRSAPrivateKey(converter.GetBytes(privateKey), out int number);
            Console.WriteLine(rsa.Encrypt(plaintext , RSAEncryptionPadding.OaepSHA256));
            Console.WriteLine(jwt.ToString());
            return jwt;
        }
    }
}
