using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album.API.Infrastructure.Messaging.Album
{
    public interface IAlbumCreatedEvent
    {
        Task Publish(Domain.Aggregates.Album album);
    }
}