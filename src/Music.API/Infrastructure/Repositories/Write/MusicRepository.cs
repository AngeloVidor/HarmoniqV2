using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music.API.Domain.Aggregates;
using Music.API.Domain.Interfaces;
using Music.API.Infrastructure.Data;

namespace Music.API.Infrastructure.Repositories.Write
{
    public class MusicRepository : IMusicRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MusicRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Domain.Aggregates.AlbumMusic music)
        {
            await _dbContext.AlbumMusic.AddAsync(music);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<AlbumMusic> GetByIdAsync(Guid id)
        {
            return await _dbContext.AlbumMusic.FindAsync(id);
        }

        public async Task UpdateAsync(AlbumMusic music)
        {
            _dbContext.AlbumMusic.Update(music);
            await _dbContext.SaveChangesAsync();
        }
    }
}