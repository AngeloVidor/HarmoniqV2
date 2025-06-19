using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album.API.Infrastructure.Messaging.Album
{
    public class ProductContent
    {
        public string CorrelationId { get; set; }
        public Guid AlbumId { get; set; }
        public Guid ProducerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}