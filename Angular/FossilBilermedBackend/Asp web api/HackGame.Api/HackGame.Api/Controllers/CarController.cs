using HackGame.Api.Data;
using HackGame.Api.Models;
using HackGame.Api.TokenAuthorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HackGame.Api.Controllers
{
    [Route("[controller]")]
    public class CarController : Controller
    {
        HackerGameDbContext _db;
        JwtAuthorization _jwt;
        public CarController(HackerGameDbContext db, JwtAuthorization jwt)
        {
            this._db = db;
            this._jwt = jwt;
        }

        [HttpGet("GetCars")]
        public async Task<IActionResult> Getcars()
        {
            List<Car> cars = await this._db.CarData.ToListAsync();
            return Ok(cars);
        }

        [HttpPost("{car}")]
        public async Task<IActionResult> PostCars(Car car)
        {
            await this._db.CarData.AddAsync(car);
            await this._db.SaveChangesAsync();
            return Ok();
        }

        [RoleAuthorizeAttribute("Admin")]
        [HttpPost("CreateCar/{rank}/{model}/{numbersold}/{percent}")]
        public async Task<IActionResult> CreateCar(int rank, string model, int numbersold, int percent)
        {
            Car car = new() { rank = rank, model = model, id = Guid.NewGuid(), numberSold = numbersold, percentageChange = percent};
            await this._db.CarData.AddAsync(car);
            await this._db.SaveChangesAsync();
            return Ok(car);
        }
        [RoleAuthorizeAttribute("Admin")]
        [HttpDelete("DeleteCar/{carId}")]
        public async Task<IActionResult> DeleteCar(Guid carId)
        {
            try
            {
                var car = await this._db.CarData.Where(i => i.id == carId).ExecuteDeleteAsync();
                return Ok(true);
            }
            catch
            {
                return BadRequest(false);
            }
        }

        [AuthorizeAttribute]
        [HttpPost("UpdateCar/{carId}/{rank}/{model}/{numbersold}/{percentchanged}")]
        public async Task<IActionResult> UpdateCar(string carId, int rank, string model, int numbersold, int percentchanged)
        {
            var car = new Car() { id = Guid.Parse(carId), rank = rank, model = model, numberSold = numbersold, percentageChange = percentchanged };
            _db.CarData.Update(car);
            await _db.SaveChangesAsync();
            return Ok(true);
        }

        [HttpGet("GetToken")]
        public async Task<IActionResult> GetToken()
        {
            return Ok(JsonSerializer.Serialize(this._jwt.GenerateJsonWebToken("user", "password")));
        }

        [AuthorizeAttribute]
        [HttpGet("Verified")]
        public async Task<IActionResult> VerifyToken()
        {
            return Ok(true);
        }
    }
}
