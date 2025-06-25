using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Infrastructure.Messaging
{
    public interface IConsumerCreatedEvent
    {
        Task Consume();
    }
}