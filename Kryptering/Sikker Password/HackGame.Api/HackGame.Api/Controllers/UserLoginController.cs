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

        /// <summary>
        /// login to user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            return Unauthorized();
        }
        
        /// <summary>
        /// creates a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CreateUser/{username}/{password}")]
        public async Task<IActionResult> CreateUser(string username, string password)
        {
            Console.WriteLine(username + password);
            CookieOptions co = new();
            co.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Append(JwtTokenName, jwtAuthorization.GenerateJsonWebToken(username,password), co);
            return Ok("welcome "+username);
        }
    }
}
