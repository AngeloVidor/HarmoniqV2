using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Domain.Entities
{
    public class Album
    {
        [Key]
        public Guid AlbumId { get; set; }
        public Guid ProducerId { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}