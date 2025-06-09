using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.API.API.DTOs
{
    public class ProducersDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
    }
}