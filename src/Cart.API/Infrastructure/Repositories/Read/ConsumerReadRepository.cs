using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Domain.Interfaces;
using Cart.API.Domain.Snapshots;
using Cart.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cart.API.Infrastructure.Repositories.Read
{
    public class ConsumerReadRepository : IConsumerReadRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ConsumerReadRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Consumer> GetByIdAsync(Guid consumerId)
        {
            return await _dbContext.Consumers.AsNoTracking().FirstOrDefaultAsync(x => x.ConsumerId == consumerId);
        }

        public async Task<Consumer> GetConsumerByUserIdAsync(Guid userId)
        {
            return await _dbContext.Consumers.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}