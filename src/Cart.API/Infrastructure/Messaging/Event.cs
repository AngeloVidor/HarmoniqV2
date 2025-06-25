using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Infrastructure.Messaging
{
    public class Event
    {
        public string CorrelantionId { get; set; }
        public Guid ConsumerId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}