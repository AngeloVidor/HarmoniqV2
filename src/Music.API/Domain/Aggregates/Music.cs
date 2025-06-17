using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Domain.Aggregates
{
    public class Music
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProducerId { get; set; }
        public Guid AlbumId { get; set; }
        public string? ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Music(Guid producerId, Guid albumId, string? imageUrl, string title, string description)
        {
            Id = Guid.NewGuid();
            ProducerId = producerId;
            AlbumId = albumId;
            ImageUrl = imageUrl;
            Title = title;
            Description = description;
            UploadedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}