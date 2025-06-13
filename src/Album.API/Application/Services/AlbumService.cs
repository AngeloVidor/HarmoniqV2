using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.API.DTOs;
using Album.API.Domain.Interfaces;

namespace Album.API.Application.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumReaderRepository _albumReaderRepository;

        public AlbumService(IAlbumReaderRepository albumReaderRepository)
        {
            _albumReaderRepository = albumReaderRepository;
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbums()
        {
            var data = await _albumReaderRepository.GetAlbumsAsync();

            var albums = new List<AlbumDto>();

            foreach (var album in data)
            {
                var dto = new AlbumDto
                {
                    Id = album.Id,
                    Title = album.Title,
                    Description = album.Description,
                    UpdatedAt = album.UpdatedAt,
                    Price = album.Price,
                    ImageUrl = album.ImageUrl
                };
                albums.Add(dto);
            }
            return albums;
        }
    }
}