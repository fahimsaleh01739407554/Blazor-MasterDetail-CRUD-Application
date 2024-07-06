using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Shared
{
    public class ServiceCenter
    {
        public int Id { get; set; }
        public string? Location { get; set; }
        public string? City { get; set; }
        public int CarId { get; set; }
    }
}
