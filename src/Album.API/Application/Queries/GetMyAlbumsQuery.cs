using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.API.DTOs;
using Amazon.Runtime.Internal;
using MediatR;

namespace Album.API.Application.Queries
{
    public record GetMyAlbumsQuery(Guid userId) : IRequest<IEnumerable<AlbumDto>>;
}