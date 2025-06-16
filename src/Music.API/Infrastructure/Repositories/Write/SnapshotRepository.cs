using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music.API.Domain.Entities;
using Music.API.Domain.Interfaces;
using Music.API.Infrastructure.Data;

namespace Music.API.Infrastructure.Repositories.Write
{
    public class SnapshotRepository : ISnapshotRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SnapshotRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync(Producer producer)
        {
            await _dbContext.ProducerSnapshots.AddAsync(producer);
            await _dbContext.SaveChangesAsync();
        }
    }
}