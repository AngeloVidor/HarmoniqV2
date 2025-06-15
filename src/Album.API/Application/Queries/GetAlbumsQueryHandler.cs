using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.API.DTOs;
using Album.API.Domain.Exceptions;
using Album.API.Domain.Interfaces;
using MediatR;

namespace Album.API.Application.Queries
{
    public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, IEnumerable<AlbumDto>>
    {
        private readonly IAlbumReaderRepository _repository;

        public GetAlbumsQueryHandler(IAlbumReaderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AlbumDto>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAlbumsAsync();
            if (data == null || !data.Any()) throw new AlbumNotFoundException();

            return data.Select(album => new AlbumDto
            {
                Id = album.Id,
                Title = album.Title,
                Description = album.Description,
                UpdatedAt = album.UpdatedAt,
                Price = album.Price,
                ImageUrl = album.ImageUrl
            });
        }
    }
}