using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Domain.Interfaces;
using Cart.API.Domain.Snapshots;
using Cart.API.Infrastructure.Data;

namespace Cart.API.Infrastructure.Repositories.Write
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ConsumerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Consumer consumer)
        {
            await _dbContext.Consumers.AddAsync(consumer);
            await _dbContext.SaveChangesAsync();
        }
    }
}