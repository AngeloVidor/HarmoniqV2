using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Music.API.Domain.Exceptions;
using Music.API.Domain.Interfaces;

namespace Music.API.Application.Queries.SingleMusic
{
    public class GetSingleMusicByIdHandler : IRequestHandler<GetSingleMusicByIdQuery, Domain.Aggregates.SingleMusic>
    {
        private readonly ISingleMusicReaderRepository _singleMusicReader;

        public GetSingleMusicByIdHandler(ISingleMusicReaderRepository singleMusicReader)
        {
            _singleMusicReader = singleMusicReader;
        }

        public async Task<Domain.Aggregates.SingleMusic> Handle(GetSingleMusicByIdQuery request, CancellationToken cancellationToken)
        {
            var singleMusic = await _singleMusicReader.GetSingleMusicByIdAsync(request.Id);
            if (singleMusic == null)
            {
                throw new MusicNotFoundException();
            }

            return singleMusic;
        }
    }
}