using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.API.API.DTOs
{
    public class CurrentProducerDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }
    }
}