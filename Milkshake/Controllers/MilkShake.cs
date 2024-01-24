using Microsoft.AspNetCore.Mvc;
using Milkshake.Model;
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

        const string apikeyCookie = "ApiKey";

        [HttpGet("GetApiKeys/{ApiKey}")]
        public async Task<IActionResult> ApikeyCreate(string ApiKey)
        {
            Response.Cookies.Append(apikeyCookie, ApiKey);
            return Ok("here is a cookie");
        }

        [HttpGet("ApiCall")]
        public async Task<IActionResult> ApiCall()
        {
            if (!Request.Cookies.ContainsKey(apikeyCookie))
            {
                return StatusCode(401, "fuck off");
            }

            string apiKey = Request.Cookies[apikeyCookie]!;
            if (!Database.Instance.Keys.ContainsKey(apiKey))
                return StatusCode(401, "fuck off");

            if (!Database.Instance.ApiCall(apiKey))
            {
                return StatusCode(401, "fuck off");
            }

            string responseString = "";
            if (!Database.Instance.Keys[apiKey].isKaptain)
            {
                foreach (var item in Database.Instance.routes.Where(i => !i.duration.Contains("år")))
                {
                    responseString += "\n"+item.ToString();
                }
                return StatusCode(200, Database.Instance.routes.Where(i=>!i.duration.Contains("år")));
            }
            else
            {
                foreach (var item in Database.Instance.routes)
                {
                    responseString += "\n"+item.ToString();
                }
                return StatusCode(200, Database.Instance.routes);
            }

        }
    }
}
