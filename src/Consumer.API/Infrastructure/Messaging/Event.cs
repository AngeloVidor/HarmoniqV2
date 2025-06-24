using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.API.Infrastructure.Messaging
{
    public class Event
    {
        public string CorrelationId { get; set; }
        public Guid ConsumerId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}