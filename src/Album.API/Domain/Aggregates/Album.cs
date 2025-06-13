using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Album.API.Domain.Aggregates
{
    public class Album
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid ProducerId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime AddedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; }

        public Album(Guid producerId, string title, string description, decimal price, string imageUrl)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Title is required", nameof(title));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description is required", nameof(description));
            if (price < 0)
                throw new InvalidOperationException("Invalid price: value cannot be less than 0.");
            if (string.IsNullOrEmpty(imageUrl))
                throw new InvalidOperationException("Image is required");

            Id = Guid.NewGuid();
            ProducerId = producerId;
            Title = title;
            Description = description;
            AddedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Price = price;
            ImageUrl = imageUrl;
        }
    }
}