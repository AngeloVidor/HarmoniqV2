using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Music.API.Application.Queries;
using Music.API.Domain.Exceptions;
using Music.API.Domain.Interfaces;

namespace Music.API.Application.Commands
{
    public class AddMusicHandler : IRequestHandler<AddMusicCommand, Guid>
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IMediator _mediator;
        private readonly IAlbumReaderRepository _albumReader;

        public AddMusicHandler(IMusicRepository musicRepository, IMediator mediator, IAlbumReaderRepository albumReader)
        {
            _musicRepository = musicRepository;
            _mediator = mediator;
            _albumReader = albumReader;
        }

        public async Task<Guid> Handle(AddMusicCommand request, CancellationToken cancellationToken)
        {
            var producer = await _mediator.Send(new GetProducerByUserIdQuery(request.UserId));
            if (producer == null)
                throw new ProducerNotFoundException();

            var album = await _albumReader.GetProducerAlbumByIdAsync(request.AlbumId, producer.ProducerId);
            if (album == null)
                throw new AlbumNotFoundException();

            var music = new Domain.Aggregates.AlbumMusic
            (
                producer.ProducerId,
                request.AlbumId,
                request.ImageUrl,
                request.Title,
                request.Description
            );

            await _musicRepository.AddAsync(music);
            return music.Id;
        }
    }
}