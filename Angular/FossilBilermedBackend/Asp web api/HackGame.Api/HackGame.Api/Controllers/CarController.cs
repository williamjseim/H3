using HackGame.Api.Data;
using HackGame.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackGame.Api.Controllers
{
    [Route("[controller]")]
    public class CarController : Controller
    {
        HackerGameDbContext _db;
        public CarController(HackerGameDbContext db)
        {
            this._db = db;
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

        [HttpPost("CreateCar/{rank}/{model}/{numbersold}/{percent}")]
        public async Task<IActionResult> CreateCar(int rank, string model, int numbersold, int percent)
        {
            Car car = new() { Rank = rank, Model = model, Id = Guid.NewGuid(), NumberSold = numbersold, PercentageChange = percent};
            await this._db.CarData.AddAsync(car);
            await this._db.SaveChangesAsync();
            return Ok(car);
        }
    }
}
