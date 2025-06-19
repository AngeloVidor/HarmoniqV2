using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Infrastructure.Messaging
{
    public interface IAlbumCreatedEvent
    {
        Task Consume();
    }
}