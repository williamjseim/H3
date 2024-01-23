using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Milkshake.Controllers
{
    [Route("api/[Controller]")]
    public class MilkShake : Controller
    {

        private readonly IConfiguration _config;

        public MilkShake(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("milkshake/{favMilkshake}")]
        public async Task<IActionResult> Index(string favMilkshake)
        {
            Response.Cookies.Append("Milkshake", favMilkshake);
            return StatusCode(200, "cookie");
        }

        [HttpGet("milkshaketwo/{favmilkshake}")]
        public async Task<IActionResult> MilkShakeTwo(string favmilkshake)
        {
            CookieOptions co = new();
            co.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Append("Milkshake", favmilkshake, co);
            return Ok(Request.Cookies["Milkshake"]);
        }
    }
}
