using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.API.Domain.Interfaces;
using Consumer.API.Infrastructure.Data;

namespace Consumer.API.Infrastructure.Repositories.Write
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ConsumerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Domain.Aggregates.Consumer consumer)
        {
            await _dbContext.AddAsync(consumer);
            await _dbContext.SaveChangesAsync();
        }
    }
}