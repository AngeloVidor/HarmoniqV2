using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Infrastructure.Messaging.Album
{
    public interface IAlbumCreatedEvent
    {
        Task Consume();
    }
}