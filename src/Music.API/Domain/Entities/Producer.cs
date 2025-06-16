using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Domain.Entities
{
    public class Producer
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProducerId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}