using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Music.API.Domain.Entities;
using Music.API.Domain.Interfaces;
using Music.API.Infrastructure.Data;

namespace Music.API.Infrastructure.Repositories.Read
{
    public class SnapshotReaderRepository : ISnapshotReaderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SnapshotReaderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Producer> GetProducerByIdAsync(Guid producerId)
        {
            return await _dbContext.ProducerSnapshots
                .FirstOrDefaultAsync(x => x.ProducerId == producerId);
        }

        public async Task<Producer> GetProducerByUserIdAsync(Guid userId)
        {
            var all = await _dbContext.ProducerSnapshots.ToListAsync();

            return await _dbContext.ProducerSnapshots
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
