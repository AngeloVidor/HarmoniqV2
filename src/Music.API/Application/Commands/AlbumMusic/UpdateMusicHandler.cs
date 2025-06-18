using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Music.API.Domain.Exceptions;
using Music.API.Domain.Interfaces;

namespace Music.API.Application.Commands.AlbumMusic
{
    public class UpdateMusicHandler : IRequestHandler<UpdateMusicCommand, bool>
    {
        private readonly IMusicRepository _musicRepository;
        private readonly ISnapshotReaderRepository _producerRepository;

        public UpdateMusicHandler(IMusicRepository musicRepository, ISnapshotReaderRepository producerRepository)
        {
            _musicRepository = musicRepository;
            _producerRepository = producerRepository;
        }

        public async Task<bool> Handle(UpdateMusicCommand request, CancellationToken cancellationToken)
        {
            var producer = await _producerRepository.GetProducerByUserIdAsync(request.userId);
            if (producer == null)
                throw new ProducerNotFoundException();

            var music = await _musicRepository.GetByIdAsync(request.id);
            if (music == null)
                throw new MusicNotFoundException();

            music.Update(request.imageUrl, request.title, request.description);
            await _musicRepository.UpdateAsync(music);
            return true;
        }
    }
}