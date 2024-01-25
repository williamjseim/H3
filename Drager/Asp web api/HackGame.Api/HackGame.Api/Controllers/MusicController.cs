using HackGame.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HackGame.Api.Controllers
{
    [Route("SecretMusic")]
    public class MusicController : Controller
    {
        //sends music to valid user
        [JwtTokenAuthorization]
        [HttpGet]
        public async Task<IActionResult> GetMusic()
        {
            string music = @"1. Dragon's Midnight Flight
                        2. Whispers of the Drage
                        3. Dragonfire Serenade
                        4. Drage Dancing under the Stars
                        5. The Dragon and the Dreamer
                        6. Echoes of a Drage's Roar
                        7. Dragon's Lullaby
                        8. Sapphire Drage Skies
                        9. The Drage's Secret Melody
                       10. Realm of the Fire Dragon";
            return Ok(music.ToString());
        }
    }
}
