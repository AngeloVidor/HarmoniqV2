using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album.API.API.DTOs
{
    public class AlbumDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}