using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Music.API.Domain.Aggregates;
using Music.API.Domain.Interfaces;
using Music.API.Infrastructure.Data;

namespace Music.API.Infrastructure.Repositories.Read
{
    public class SingleMusicReaderRepository : ISingleMusicReaderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SingleMusicReaderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SingleMusic> GetProducerSingleMusicById(Guid producerId, Guid musicId)
        {
            return await _dbContext.SingleMusics
                .FirstOrDefaultAsync(x => x.Id == musicId && x.ProducerId == producerId);
        }

        public async Task<SingleMusic> GetSingleMusicByIdAsync(Guid id)
        {
            return await _dbContext.SingleMusics
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}