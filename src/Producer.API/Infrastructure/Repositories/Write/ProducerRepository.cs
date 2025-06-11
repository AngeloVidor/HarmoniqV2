using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AddAsync(Domain.Aggregates.Producer producer)
        {
            await _dbContext.Producers.AddAsync(producer);
            int lines = await _dbContext.SaveChangesAsync();
            return lines > 0;
        }

        public async Task UpdateAsync(Domain.Aggregates.Producer producer)
        {
            _dbContext.Update(producer);
            await _dbContext.SaveChangesAsync();
        }
    }
}