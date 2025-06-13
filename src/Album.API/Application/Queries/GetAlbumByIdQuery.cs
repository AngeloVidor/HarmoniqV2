using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.API.DTOs;
using Album.API.Domain.Aggregates;
using Amazon.Runtime.Internal;
using MediatR;

namespace Album.API.Application.Queries
{
    public record GetAlbumByIdQuery(Guid id) : IRequest<AlbumDto>;
}