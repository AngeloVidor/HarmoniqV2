using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Interfaces;
using Album.API.Domain.Snapshots;
using Album.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Album.API.Infrastructure.Repositories.Read
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProducerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProducerSnap> GetProducerByUserId(Guid userId)
        {
            return await _dbContext.ProducerSnapshots.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}