using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.API.DTOs;

namespace Album.API.Domain.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDto>> GetAlbums();
    }
}