using System.Diagnostics;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using HackGame.Api.Filters;
using HackGame.Api.TokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackGame.Api.Controllers
{
    [Route("/")]
    [AllowAnonymous]
    public class ImageController : Controller
    {
        JwtAuthorization _jwt;
        public ImageController(JwtAuthorization jwt)
        {
            this._jwt = jwt;
        }
        private List<string> _images = new();
        [HttpGet("/Image")]
        public async Task<IActionResult> ImageGet()
        {
            _images.Add("adwasdws");
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

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(string Username, string Password){
            var token = this._jwt.GenerateJsonWebToken(Username, Password);
            Console.WriteLine(token);
            return Ok(token);
        }

        [HttpPost("/Motor")]
        public async Task<IActionResult> Motor()
        {
            // See documentation on defining a message payload.
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    { "score", "850" },
                    { "time", "2:45" },
                },
                Notification = new Notification() { Body = "erasdafea", Title = "new message" },
                Topic = "Topic",
                Android = new AndroidConfig() { Priority = Priority.Normal, Notification = new() { Sound = "motorsound", ChannelId="2" } },
                //Android = new AndroidConfig() { Priority = Priority.Normal, Notification = new() { Sound = "f1_notification", ChannelId="3" } },
            };

            // Send a message to the device corresponding to the provided
            // registration token.
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return Ok();
        }

        [HttpPost("/Notification")]
        public async Task<IActionResult> Notification()
        {
            // See documentation on defining a message payload.
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    { "score", "850" },
                    { "time", "2:45" },
                },
                Notification = new Notification() { Body = "erasdafea", Title = "new message" },
                Topic = "Topic",
                Android = new AndroidConfig() { Priority = Priority.Normal, Notification = new() { Sound = "f1_notification", ChannelId="3" } },
            };

            // Send a message to the device corresponding to the provided
            // registration token.
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return Ok();
        }

    }
}
