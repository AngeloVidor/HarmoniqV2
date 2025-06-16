using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task AddAsync(Domain.Aggregates.Music music)
        {
            await _dbContext.Musics.AddAsync(music);
            await _dbContext.SaveChangesAsync();
        }
    }
}