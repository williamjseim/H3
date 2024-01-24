using Microsoft.AspNetCore.Mvc;
using Milkshake.Model;
using System.Net;

namespace Milkshake.Controllers
{
    [Route("api/[Controller]")]
    public class MilkShake : Controller
    {
        const string apikeyCookie = "ApiKey";
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

        //creates an api key based on a string 
        [HttpGet("GetApiKeys/{ApiKey}")]
        public async Task<IActionResult> ApikeyCreate(string ApiKey)
        {
            Response.Cookies.Append(apikeyCookie, ApiKey);
            return Ok("Here's is a cookie. Now fuck off");
        }

        //gets all routes that you are intitled to
        [HttpGet("ApiCall")]
        public async Task<IActionResult> ApiCall()
        {
            if (!Request.Cookies.ContainsKey(apikeyCookie))
            {
                return StatusCode(401, "fuck off");
            }

            string apiKey = Request.Cookies[apikeyCookie]!;
            if(Database.Instance.IsApiKeyValid(apiKey))
            {
                Database.Instance.ApiCall(apiKey);
                if (Database.Instance.Keys[apiKey].isKaptain)
                {
                    return Ok(Database.Instance.routes);
                }
                return Ok(Database.Instance.routes.Where(i=>i.secret == false));
            }
            return StatusCode(401, "fuck off");
        }
    }
}
