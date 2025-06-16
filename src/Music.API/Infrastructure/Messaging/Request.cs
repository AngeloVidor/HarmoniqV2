using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Infrastructure.Messaging
{
    public class Request
    {
        public string CorrelationId { get; set; }
        public Guid ProducerId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}