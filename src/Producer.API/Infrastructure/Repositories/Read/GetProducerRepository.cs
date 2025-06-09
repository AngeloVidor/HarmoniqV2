using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Producer.API.Domain.Interfaces;
using Producer.API.Infrastructure.Data;

namespace Producer.API.Infrastructure.Repositories.Read
{
    public class GetProducerRepository : IGetProducerRepoitory
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProducerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Aggregates.Producer> GetProducerByUserIdAsync(Guid id)
        {
            return await _dbContext.Producers
                .FirstOrDefaultAsync(p => p.UserId == id);
        }

        public async Task<IEnumerable<Domain.Aggregates.Producer>> GetProducersAsync()
        {
            return await _dbContext.Producers.ToListAsync();

        }
    }
}