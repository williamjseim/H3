using Microsoft.AspNetCore.Mvc;

namespace Milkshake.Controllers
{
    [Route("api/[Controller]")]
    public class MilkMachine : Controller
    {
        [HttpGet("kill")]
        public IActionResult Index()
        {
            return Ok();
        }
        
        [HttpGet("remove")]
        public IActionResult Remove()
        {
            CookieOptions co = new();
            co.Expires = DateTime.Now;
            Response.Cookies.Append("milkshake", "", co);
            return Ok();
        }



    }
}
