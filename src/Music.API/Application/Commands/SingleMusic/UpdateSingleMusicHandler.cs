using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Music.API.Domain.Exceptions;
using Music.API.Domain.Interfaces;

namespace Music.API.Application.Commands.SingleMusic
{
    public class UpdateSingleMusicHandler : IRequestHandler<UpdateSingleMusicCommand, bool>
    {
        private readonly ISingleMusicRepository _singleMusicRepository;
        private readonly ISingleMusicReaderRepository _singleMusicReaderRepository;
        private readonly ISnapshotReaderRepository _producerRepository;


        public UpdateSingleMusicHandler(ISingleMusicRepository singleMusicRepository, ISingleMusicReaderRepository singleMusicReaderRepository, ISnapshotReaderRepository producerRepository)
        {
            _singleMusicRepository = singleMusicRepository;
            _singleMusicReaderRepository = singleMusicReaderRepository;
            _producerRepository = producerRepository;
        }

        public async Task<bool> Handle(UpdateSingleMusicCommand request, CancellationToken cancellationToken)
        {
            var producer = await _producerRepository.GetProducerByUserIdAsync(request.UserId);
            if (producer == null)
                throw new ProducerNotFoundException();

            var music = await _singleMusicReaderRepository.GetProducerSingleMusicById(producer.ProducerId, request.Id);
            if (music == null)
                throw new MusicNotFoundException();

            music.Update(request.ImageUrl, request.Title, request.Description);
            await _singleMusicRepository.UpdateAsync(music);

            return true;

        }
    }
}