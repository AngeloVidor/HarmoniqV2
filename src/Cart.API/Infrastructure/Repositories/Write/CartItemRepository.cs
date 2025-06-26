using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Domain.Aggregates;
using Cart.API.Domain.Interfaces;
using Cart.API.Infrastructure.Data;

namespace Cart.API.Infrastructure.Repositories.Write
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CartItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(CartItem item)
        {
            await _dbContext.CartItems.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}