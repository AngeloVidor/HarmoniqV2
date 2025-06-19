using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.API.Domain.Aggregates;
using Product.API.Domain.Interfaces;
using Product.API.Infrastructure.Data;

namespace Product.API.Infrastructure.Repositories.Write
{
    public class AlbumProductRepository : IAlbumProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AlbumProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync(AlbumProduct product)
        {
            await _dbContext.Albums.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}