using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Interfaces;
using Album.API.Domain.Snapshots;
using Album.API.Infrastructure.Data;

namespace Album.API.Infrastructure.Repositories
{
    public class ProducerSnapshotRepository : IProducerSnapshotRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProducerSnapshotRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync(ProducerSnap snapshot)
        {
            await _dbContext.ProducerSnapshots.AddAsync(snapshot);
            await _dbContext.SaveChangesAsync();
        }
    }
}