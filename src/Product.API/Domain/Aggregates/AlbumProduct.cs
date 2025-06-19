using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Domain.Aggregates
{
    public class AlbumProduct
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AlbumId { get; set; }
        public Guid ProducerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public AlbumProduct(Guid albumId, Guid producerId, string title, string description, decimal price, string imageUrl)
        {
            Id = Guid.NewGuid();
            AlbumId = albumId;
            ProducerId = producerId;
            Title = title;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            CreatedAt = DateTime.UtcNow;
        }
    }
}