using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Producer.API.Domain.Interfaces;
using Producer.API.Infrastructure.Data;

namespace Producer.API.Infrastructure.Repositories
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProducerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Domain.Aggregates.Producer producer)
        {
            await _dbContext.Producers.AddAsync(producer);
            await _dbContext.SaveChangesAsync();
        }
    }
}