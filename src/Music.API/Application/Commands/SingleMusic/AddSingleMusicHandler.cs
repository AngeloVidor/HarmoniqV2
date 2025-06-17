using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.VisualBasic;
using Music.API.Domain.Exceptions;
using Music.API.Domain.Interfaces;

namespace Music.API.Application.Commands.SingleMusic
{
    public class AddSingleMusicHandler : IRequestHandler<AddSingleMusicCommand, Guid>
    {
        private readonly ISingleMusicRepository _singleMusicRepository;
        private readonly ISnapshotReaderRepository _snapshotReaderRepository;

        public AddSingleMusicHandler(ISingleMusicRepository singleMusicRepository, ISnapshotReaderRepository snapshotReaderRepository)
        {
            _singleMusicRepository = singleMusicRepository;
            _snapshotReaderRepository = snapshotReaderRepository;
        }

        public async Task<Guid> Handle(AddSingleMusicCommand request, CancellationToken cancellationToken)
        {
            var producer = await _snapshotReaderRepository.GetProducerByUserIdAsync(request.UserId);
            if (producer == null)
                throw new ProducerNotFoundException();

            var music = new Domain.Aggregates.SingleMusic
            (
                request.Title,
                request.Description,
                request.ImageUrl,
                producer.ProducerId
            );

            await _singleMusicRepository.AddAsync(music);
            return music.Id;
        }
    }
}










