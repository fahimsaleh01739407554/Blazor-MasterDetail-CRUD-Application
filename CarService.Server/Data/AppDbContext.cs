using CarService.Shared;
using Microsoft.EntityFrameworkCore;

namespace CarService.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<ServiceCenter> ServiceCenters { get; set; }
    }

}
