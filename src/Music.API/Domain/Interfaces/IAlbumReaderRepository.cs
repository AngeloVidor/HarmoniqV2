using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music.API.Domain.Aggregates;
using Music.API.Domain.Entities;

namespace Music.API.Domain.Interfaces
{
    public interface IAlbumReaderRepository
    {
        Task<Album> GetProducerAlbumByIdAsync(Guid id, Guid producerId);
        Task<IEnumerable<AlbumMusic>> GetAlbumMusicsByAlbumIdAsync(Guid albumId, Guid producerId);
        Task<Album> GetAlbumByIdAsync(Guid albumId);
    }
}
