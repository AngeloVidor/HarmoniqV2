
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album.API.Infrastructure.Messaging.Album
{
    public class AlbumContent
    {
        public string CorrelationId { get; set; }
        public Guid Id { get; set; }
        public Guid ProducerId { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}