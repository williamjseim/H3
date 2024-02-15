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
            Car car = new() { rank = rank, model = model, id = Guid.NewGuid(), numberSold = numbersold, percentageChange = percent};
            await this._db.CarData.AddAsync(car);
            await this._db.SaveChangesAsync();
            return Ok(car);
        }

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

        [HttpPost("UpdateCar/{carId}/{rank}/{model}/{numbersold}/{percentchanged}")]
        public async Task<IActionResult> UpdateCar(string carId, int rank, string model, int numbersold, int percentchanged)
        {
            var car = new Car() { id = Guid.Parse(carId), rank = rank, model = model, numberSold = numbersold, percentageChange = percentchanged };
            _db.CarData.Update(car);
            await _db.SaveChangesAsync();
            return Ok(true);
        }
    }
}
