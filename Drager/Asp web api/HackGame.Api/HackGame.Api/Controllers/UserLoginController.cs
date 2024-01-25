using HackGame.Api.Data;
using HackGame.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using HackGame.Api.Filters;
using HackGame.Api.TokenAuthorization;
using System.Net;

namespace HackGame.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : Controller
    {
        const string JwtTokenName = "JwtToken";
        
        private readonly HackerGameDbContext _db;
        private readonly IConfiguration _config;
        private readonly JwtAuthorization jwtAuthorization;
        public UserLoginController(HackerGameDbContext hackerGameDbContext, IConfiguration config)
        {
            this._db = hackerGameDbContext;
            _config = config;
            jwtAuthorization = new(config, hackerGameDbContext);
        }

        //login to user
        [AllowAnonymous]
        [HttpGet("{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if(Database.Instance.Login(username, password))
            {
                CookieOptions co = new();
                co.Expires = DateTime.Now.AddMinutes(5);
                Response.Cookies.Append(JwtTokenName, jwtAuthorization.GenerateJsonWebToken(username, password), co);
                return Ok("Welcome " + username);
            }
            return Unauthorized();
        }
        
        //creates a user
        [AllowAnonymous]
        [HttpPost("CreateUser/{username}/{password}")]
        public async Task<IActionResult> CreateUser(string username, string password)
        {
            if(Database.Instance.CreateNewUser(username, password))
            {
                Console.WriteLine(username + password);
                CookieOptions co = new();
                co.Expires = DateTime.Now.AddMinutes(5);
                Response.Cookies.Append(JwtTokenName, jwtAuthorization.GenerateJsonWebToken(username,password), co);
                return Ok("welcome "+username);
            }
            return BadRequest("fuck off");
        }

        //quick way of checking if jwt token is valid
        [JwtTokenAuthorization]
        [HttpGet("/VerifyLogin")]
        public async Task<IActionResult> VerifyUser()
        {
            if (CheckToken())
            {
                return Ok(true);
            }
            return Ok(false);
        }

        //checks if token is valid
        private bool CheckToken()
        {
            try
            {
                string token = Request.Cookies[JwtTokenName];
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                TokenValidationParameters parameters = new TokenValidationParameters {
                    ValidIssuer = _config["JwtSettings:Issuer"],
                    ValidAudience = _config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateActor = false,
                };
                var result = handler.ValidateToken(token, parameters, out SecurityToken validatedToken);
                return true;
            }
            catch { return false; }
        }
    }
}
