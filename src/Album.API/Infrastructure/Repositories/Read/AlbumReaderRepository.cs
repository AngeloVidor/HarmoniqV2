using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Album.API.Domain.Interfaces;
using Album.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Album.API.Infrastructure.Repositories.Read
{
    public class AlbumReaderRepository : IAlbumReaderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AlbumReaderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Aggregates.Album> GetAlbumByIdAsync(Guid id)
        {
            return await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Domain.Aggregates.Album>> GetAlbumsAsync()
        {
            return await _dbContext.Albums.ToListAsync();
        }
    }
}