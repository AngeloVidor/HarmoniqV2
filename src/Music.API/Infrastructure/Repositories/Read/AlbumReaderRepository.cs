using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Music.API.Domain.Entities;
using Music.API.Domain.Interfaces;
using Music.API.Infrastructure.Data;

namespace Music.API.Infrastructure.Repositories.Read
{
    public class AlbumReaderRepository : IAlbumReaderRepository
    {

        private readonly ApplicationDbContext _dbContext;
        public AlbumReaderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Album> GetAlbumByIdAsync(Guid id)
        {
            return await _dbContext.AlbumSnapshots.FirstOrDefaultAsync(x => x.AlbumId == id);
        }
    }
}