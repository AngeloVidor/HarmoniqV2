using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using MediatR;
using Music.API.API.DTOs;

namespace Music.API.Application.Queries.AlbumMusics
{
    public record GetAlbumMusicsQuery(Guid AlbumId, Guid ProducerId) : IRequest<IEnumerable<AlbumMusicDto>>;
}