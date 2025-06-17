using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Infrastructure.Messaging
{
    public interface IProducerCreatedEvent
    {
        Task Consume();
    }
}