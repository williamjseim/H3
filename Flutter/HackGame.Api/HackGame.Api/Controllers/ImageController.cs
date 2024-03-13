using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackGame.Api.Controllers
{
    [Route("/")]
    [AllowAnonymous]
    public class ImageController : Controller
    {
        private List<string> _images = new();
        [HttpGet("/Image")]
        public async Task<IActionResult> ImageGet()
        {
            _images.Add("adwasdws");
            Console.WriteLine("asdwawds");
            return Ok("answer");
        }

        [HttpPost("/Image")]
        public async Task<IActionResult> ImagePost(string Image)
        {
            _images.Add(Image);
            Console.WriteLine(Image);
            return Ok("Image saved");
        }

        private string GetQueryData(string query){
            string? answer = this.HttpContext.Request.Query.Where(i=>i.Key == query).First().Value;
            return answer == null ? "" : answer;
        }
    }
}
