using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Music.API.Domain.Aggregates;
using Music.API.Domain.Entities;
using Music.API.Domain.Interfaces;
using Music.API.Infrastructure.Data;

namespace Music.API.Infrastructure.Repositories.Read
{
    public class AlbumReaderRepository : IAlbumReaderRepository
    {

        private readonly ApplicationDbContext _dbContext;
        public AlbumReaderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Album> GetProducerAlbumByIdAsync(Guid id, Guid producerId)
        {
            return await _dbContext.AlbumSnapshots.FirstOrDefaultAsync(x => x.AlbumId == id && x.ProducerId == producerId);
        }

        public async Task<IEnumerable<AlbumMusic>> GetAlbumMusicsByAlbumIdAsync(Guid albumId, Guid producerId)
        {
            return await _dbContext.AlbumMusic
                .Where(x => x.AlbumId == albumId && x.ProducerId == producerId)
                .ToListAsync();
        }

        public async Task<Album> GetAlbumByIdAsync(Guid albumId)
        {
            return await _dbContext.AlbumSnapshots
                .FirstOrDefaultAsync(x => x.AlbumId == albumId);
        }
    }
}