using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.API.Domain.Interfaces;
using Consumer.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Consumer.API.Infrastructure.Repositories.Read
{
    public class ConsumerReadRepository : IConsumerReadRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ConsumerReadRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Aggregates.Consumer> GetConsumerByUserIdAsync(Guid userId)
        {
            return await _dbContext.Consumers.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}