using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.API.Infrastructure.Messaging
{
    public interface IConsumerCreatedEvent
    {
        Task Publish(Domain.Aggregates.Consumer consumer);
    }
}