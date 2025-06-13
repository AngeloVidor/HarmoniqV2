using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.API.DTOs;
using MediatR;

namespace Album.API.Application.Queries
{
    public record GetAlbumsQuery() : IRequest<IEnumerable<AlbumDto>>;




}