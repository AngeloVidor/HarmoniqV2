using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Interfaces;
using Album.API.Infrastructure.Data;

namespace Album.API.Infrastructure.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AlbumRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Domain.Aggregates.Album album)
        {
            await _dbContext.Albums.AddAsync(album);
            await _dbContext.SaveChangesAsync();
        }
    }
}