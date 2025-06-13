using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.API.DTOs;
using Album.API.Domain.Interfaces;
using MediatR;

namespace Album.API.Application.Queries
{
    public class GetAlbumByIdHandler : IRequestHandler<GetAlbumByIdQuery, AlbumDto>
    {
        private readonly IAlbumReaderRepository _albumReaderRepository;

        public GetAlbumByIdHandler(IAlbumReaderRepository albumReaderRepository)
        {
            _albumReaderRepository = albumReaderRepository;
        }

        public async Task<AlbumDto> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
        {
            var album = await _albumReaderRepository.GetAlbumByIdAsync(request.id);

            var albumDto = new AlbumDto
            {
                Id = album.Id,
                Title = album.Title,
                Description = album.Description,
                UpdatedAt = album.UpdatedAt,
                Price = album.Price,
                ImageUrl = album.ImageUrl
            };

            if (album == null)
                throw new KeyNotFoundException($"Album with id {request.id} not found");

            return albumDto;
        }
    }
}