using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Interfaces;
using Album.API.Infrastructure.Messaging.Album;
using MediatR;

namespace Album.API.Application.Commands
{
    public class AddAlbumHandler : IRequestHandler<AddAlbumCommand, Guid>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IAlbumCreatedEvent @event;

        public AddAlbumHandler(IAlbumRepository albumRepository, IAlbumCreatedEvent @event)
        {
            _albumRepository = albumRepository;
            this.@event = @event;
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

            await @event.Publish(album);

            return album.Id;
        }
    }
}