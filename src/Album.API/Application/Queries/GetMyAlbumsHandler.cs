using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.API.DTOs;
using Album.API.Domain.Interfaces;
using MediatR;

namespace Album.API.Application.Queries
{
    public class GetMyAlbumsHandler : IRequestHandler<GetMyAlbumsQuery, IEnumerable<AlbumDto>>
    {
        private readonly IAlbumReaderRepository _albumReader;
        private readonly IProducerService _producerService;

        public GetMyAlbumsHandler(IAlbumReaderRepository albumReader, IProducerService producerService)
        {
            _albumReader = albumReader;
            _producerService = producerService;
        }

        public async Task<IEnumerable<AlbumDto>> Handle(GetMyAlbumsQuery request, CancellationToken cancellationToken)
        {
            var producer = await _producerService.GetProducerByUserId(request.userId);
            if (producer == null)
                throw new KeyNotFoundException("Producer not found");

            var albums = await _albumReader.GetMyAlbums(producer.ProducerId);
            if (albums == null || !albums.Any())
                throw new InvalidOperationException($"No albums found to {producer.ProducerId}");

            var response = albums.Select(album => new AlbumDto
            {
                Id = album.Id,
                Title = album.Title,
                Description = album.Description,
                ImageUrl = album.ImageUrl,
                UpdatedAt = album.UpdatedAt,
                Price = album.Price
            }).ToList();

            return response;
        }
    }
}