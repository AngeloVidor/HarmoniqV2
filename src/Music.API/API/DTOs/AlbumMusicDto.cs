using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.API.DTOs
{
    public class AlbumMusicDto
    {
        public Guid AlbumId { get; set; }
        public Guid MusicId { get; set; }
        public Guid ProducerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}