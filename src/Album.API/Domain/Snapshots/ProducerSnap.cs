using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Album.API.Domain.Snapshots
{
    public class ProducerSnap
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProducerId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}