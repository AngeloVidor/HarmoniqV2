using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Aggregates;

namespace Album.API.Domain.Interfaces
{
    public interface IAlbumReaderRepository
    {
        Task<IEnumerable<Domain.Aggregates.Album>> GetAlbumsAsync();
        Task<Domain.Aggregates.Album> GetAlbumByIdAsync(Guid id);

        Task<IEnumerable<Domain.Aggregates.Album>> GetMyAlbums(Guid producerId);
    }
}