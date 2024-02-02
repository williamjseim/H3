using DinDigitaleVerden.Data;
using DinDigitaleVerden.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DinDigitaleVerden.Controllers
{
    [Route("[Controller]")]
    public class LoginController : Controller
    {
        IConfiguration _config;
        VerdenDbContext _db;
        Jwt _jwt;
        Logger _logger;
        public LoginController(IConfiguration config, VerdenDbContext db, Jwt jwt, Logger logger)
        {
            this._config = config;
            this._db = db;
            this._jwt = jwt;
            this._logger = logger;
        }

        /// <summary>
        /// Creates a user in the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("CreateUser/{username}/{password}/{role}")]
        public async Task<IActionResult> CreateUser(string username, string password, Roles role)
        {
            try
            {
                if (!InputValidator.ValidateString(username) || !InputValidator.ValidateString(password))
                    return Unauthorized("Illegal characters");
                if(await _db.User_Data.Where(i=>i.Username == username).AnyAsync())
                    return BadRequest("Username exists");

                UserModel newUser = new UserModel(username, PasswordHasher.Hash(password), role);
                await this._db.AddAsync(newUser);
                await this._db.SaveChangesAsync();
                return Ok("user created");
            }
            catch (Exception ex)
            {
                //log exception
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// checks if the username and
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("Login/{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                if (!InputValidator.ValidateString(username) || !InputValidator.ValidateString(password))
                    return Unauthorized("Illegal characters");
                UserModel user = await _db.User_Data.Where(i => i.Username == username).FirstOrDefaultAsync();
                if(user == null)
                    return BadRequest("Wrong username or password");
                if(user.Password == PasswordHasher.Hash(password))
                {
                    CookieOptions co = new() { Expires = DateTime.Now.AddDays(1), IsEssential = true, HttpOnly = true, };
                    Response.Cookies.Append(_config["Jwt:CookieName"], _jwt.GenerateJwtToken(user), co);
                    return Ok("Welcome " + username);
                }
                return Unauthorized("Wrong username or password");
                }
            catch (Exception e)
            {
                this._logger.Log(e.ToString()).ConfigureAwait(false);
                return BadRequest("Failed to login");
            }
        }

        [JwtRoleFilter(Roles.Admin)]
        [HttpGet($"{nameof(AdminStuff)}")]
        public async Task<IActionResult> AdminStuff()
        {
            try
            {
                return Ok("Admin Permission");
            }
            catch (Exception e)
            {
                this._logger.Log(e.ToString()).ConfigureAwait(false);
                return BadRequest("Something went wrong");
            }
        }

        [JwtRoleFilter(Roles.User)]
        [HttpGet($"{nameof(UserStuff)}")]
        public async Task<IActionResult> UserStuff()
        {
            try
            {
                return Ok("User Permission");
            }
            catch (Exception e)
            {
                this._logger.Log(e.ToString()).ConfigureAwait(false);
                return BadRequest("Something went wrong");
            }
        }
    }
}
