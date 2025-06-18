using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Music.API.Domain.Aggregates
{
    public class SingleMusic
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid ProducerId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime AddedAt { get; private set; }
        public string? ImageUrl { get; private set; }

        public SingleMusic(string title, string description, string? imageUrl, Guid producerId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            ProducerId = producerId;
            AddedAt = DateTime.UtcNow;
        }

        public void Update(string? imageUrl, string title, string description)
        {
            ImageUrl = imageUrl;
            Title = title;
            Description = description;
        }
    }
}