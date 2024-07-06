using CarService.Server.Data;
using CarService.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CarsController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Car>>> Get()
        {
            return await _db.Cars.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            var car = await _db.Cars.Include(s => s.ServiceCenters).FirstOrDefaultAsync(s => s.Id == id);
            if (car == null) { return NotFound(); }
            return car;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Car car)
        {
            _db.Cars.Add(car);
            await _db.SaveChangesAsync();
            return car.Id;
        }
        [HttpPut]
        public async Task<ActionResult> Put(Car car)
        {
            _db.Entry(car).State = EntityState.Modified;
            foreach (var item in car.ServiceCenters)
            {
                if (item.Id != 0)
                {
                    _db.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    _db.Entry(item).State = EntityState.Added;
                }
            }
            var idsOfServiceCenter = car.ServiceCenters.Select(s => s.Id).ToList();
            var ServiceCenterToDelete = await _db.ServiceCenters.Where(s => !idsOfServiceCenter.Contains(s.Id) && s.CarId == car.Id).ToListAsync();
            _db.RemoveRange(ServiceCenterToDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var car = await _db.Cars.Include(s => s.ServiceCenters).FirstOrDefaultAsync(s => s.Id == id);
            if (car == null) return NotFound();
            _db.ServiceCenters.RemoveRange(car.ServiceCenters);
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }

}
