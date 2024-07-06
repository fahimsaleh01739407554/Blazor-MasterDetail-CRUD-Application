using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Shared
{
    public class Car
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public DateTime ManufactureDate { get; set; } = DateTime.Now;
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }
        public List<ServiceCenter> ServiceCenters { get; set; } = new List<ServiceCenter>();
    }

}
