using Microsoft.AspNetCore.Mvc;

namespace HackGame.Api.Controllers
{
    [Route("/")]
    public class ImageController : Controller
    {
        private List<string> _images = new();
        [HttpGet("Image/{base64Image}")]
        public async Task<IActionResult> ImageGet(string base64Image)
        {
            _images.Add(base64Image);
            return Ok();
        }

        [HttpPost("Image/{base64Image}")]
        public async Task<IActionResult> ImagePost(string base64Image)
        {
            Console.WriteLine(base64Image);
            return Ok(_images);
        }
    }
}
