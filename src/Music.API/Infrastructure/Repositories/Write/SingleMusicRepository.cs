using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music.API.Domain.Aggregates;
using Music.API.Domain.Interfaces;
using Music.API.Infrastructure.Data;

namespace Music.API.Infrastructure.Repositories.Write
{
    public class SingleMusicRepository : ISingleMusicRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SingleMusicRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(SingleMusic music)
        {
            await _dbContext.SingleMusics.AddAsync(music);
            await _dbContext.SaveChangesAsync();
        }
    }
}