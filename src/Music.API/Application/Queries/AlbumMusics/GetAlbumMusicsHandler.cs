using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Music.API.API.DTOs;
using Music.API.Domain.Exceptions;
using Music.API.Domain.Interfaces;

namespace Music.API.Application.Queries.AlbumMusics
{
    public class GetAlbumMusicsHandler : IRequestHandler<GetAlbumMusicsQuery, IEnumerable<AlbumMusicDto>>
    {
        private readonly IAlbumReaderRepository _albumReaderRepository;
        private readonly ISnapshotReaderRepository _producerReaderRepository;

        public GetAlbumMusicsHandler(IAlbumReaderRepository albumReaderRepository, ISnapshotReaderRepository producerReaderRepository)
        {
            _albumReaderRepository = albumReaderRepository;
            _producerReaderRepository = producerReaderRepository;
        }

        public async Task<IEnumerable<AlbumMusicDto>> Handle(GetAlbumMusicsQuery request, CancellationToken cancellationToken)
        {
            var producer = await _producerReaderRepository.GetProducerByIdAsync(request.ProducerId);
            if (producer == null)
                throw new ProducerNotFoundException();

            var album = await _albumReaderRepository.GetAlbumByIdAsync(request.AlbumId);
            if (album == null)
                throw new AlbumNotFoundException();

            var albumMusics = await _albumReaderRepository.GetAlbumMusicsByAlbumIdAsync(request.AlbumId, producer.ProducerId);
            if (albumMusics == null || !albumMusics.Any())
                throw new AlbumMusicsNotFoundException();

            return albumMusics.Select(m => new AlbumMusicDto
            {
                MusicId = m.Id,
                AlbumId = m.AlbumId,
                ProducerId = m.ProducerId,
                Title = m.Title,
                Description = m.Description,
                ImageUrl = m.ImageUrl
            }).ToList();
        }
    }
}