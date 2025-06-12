using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Interfaces;
using MediatR;

namespace Album.API.Application.Commands
{
    public class AddAlbumHandler : IRequestHandler<AddAlbumCommand, Guid>
    {
        private readonly IAlbumRepository _albumRepository;

        public AddAlbumHandler(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<Guid> Handle(AddAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = new Domain.Aggregates.Album
            (
                request.ProducerId,
                request.Title,
                request.Description,
                request.Price,
                request.ImageUrl
            );
            await _albumRepository.AddAsync(album);
            return album.Id;
        }
    }
}