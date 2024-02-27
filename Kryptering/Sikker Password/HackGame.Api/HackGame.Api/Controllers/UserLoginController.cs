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
            var user = await this._db.Login_Data.Where(i => i.Username == username).FirstOrDefaultAsync();//gets the first user that matches the username
            if(user == null)
                return Unauthorized("Username or password is wrong");

            var hashedPass = PasswordHasher.HashPassword(password, _config["Salt"]!);// recreates hashed password
            if(user.Password == hashedPass)
            {
                CookieOptions co = new() { IsEssential = true };
                Response.Cookies.Append(JwtTokenName, jwtAuthorization.GenerateJsonWebToken(username,password), co);//generates a jwt token for user
                return Ok(user);
            }

            return Unauthorized("Username or password is wrong");
        }
        
        /// <summary>
        /// creates a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CreateUser/{username}/{password}")]
        public async Task<IActionResult> CreateUser(string username, string password)//this doesnt check for already existing users
        {
            Console.WriteLine(username + password);
            CookieOptions co = new();
            co.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Append(JwtTokenName, jwtAuthorization.GenerateJsonWebToken(username,password), co);//generates a jwt token for user
            UserData user = new()
            {
                Username = username,
                Password = PasswordHasher.HashPassword(password, _config["Salt"]!),//Gets salt from appconfig and gives it to the passwordhasher to hash the password
                Id = Guid.NewGuid(),
            };
            await _db.AddAsync(user);
            await _db.SaveChangesAsync();
            return Ok("welcome "+username);
        }
    }
}
