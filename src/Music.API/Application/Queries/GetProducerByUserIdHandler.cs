using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Music.API.Domain.Entities;
using Music.API.Domain.Interfaces;

namespace Music.API.Application.Queries
{
    public class GetProducerByUserIdHandler : IRequestHandler<GetProducerByUserIdQuery, Producer>
    {
        private readonly ISnapshotReaderRepository _readerRepository;

        public GetProducerByUserIdHandler(ISnapshotReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        public async Task<Producer> Handle(GetProducerByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _readerRepository.GetProducerByUserIdAsync(request.userId);
        }
    }
}