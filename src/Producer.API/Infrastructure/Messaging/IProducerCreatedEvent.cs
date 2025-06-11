using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Producer.API.Domain.Events;

namespace Producer.API.Infrastructure.Messaging
{
    public interface IProducerCreatedEvent
    {
        Task Publish(Guid producerId, Guid userId, string name);
    }
}