using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Music.API.Application.Queries;
using Music.API.Domain.Interfaces;

namespace Music.API.Application.Commands
{
    public class AddMusicHandler : IRequestHandler<AddMusicCommand, Guid>
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IMediator _mediator;

        public AddMusicHandler(IMusicRepository musicRepository, IMediator mediator)
        {
            _musicRepository = musicRepository;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(AddMusicCommand request, CancellationToken cancellationToken)
        {
            var producer = await _mediator.Send(new GetProducerByUserIdQuery(request.UserId));

            var music = new Domain.Aggregates.Music
            (
                producer.ProducerId,
                request.AlbumId,
                request.IsPartOfAlbum,
                request.ImageUrl,
                request.Title,
                request.Description
            );

            await _musicRepository.AddAsync(music);
            return music.Id;
        }
    }
}