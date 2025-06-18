using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music.API.Domain.Aggregates;

namespace Music.API.Domain.Interfaces
{
    public interface IMusicRepository
    {
        Task AddAsync(Aggregates.AlbumMusic music);
        Task UpdateAsync(Aggregates.AlbumMusic music);
        Task<AlbumMusic> GetByIdAsync(Guid id);
    }
}