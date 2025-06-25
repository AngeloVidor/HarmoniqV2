using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Domain.Interfaces;
using Cart.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cart.API.Infrastructure.Repositories.Read
{
    public class CartReaderRepository : ICartReaderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CartReaderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Aggregates.Cart> GetCartByConsumerIdAsync(Guid consumerId)
        {
            return await _dbContext.Carts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ConsumerId == consumerId);
        }
    }
}