using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.API.Domain.Interfaces;
using Product.API.Infrastructure.Data;

namespace Product.API.Infrastructure.Repositories.Read
{
    public class ProductReaderRepository : IProductReaderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductReaderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Aggregates.Product> GetProductByIdAsync(Guid productId)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}