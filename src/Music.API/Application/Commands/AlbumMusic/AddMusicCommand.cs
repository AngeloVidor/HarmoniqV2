using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Music.API.Application.Commands
{
    public record AddMusicCommand
    (
        Guid ProducerId,
        Guid AlbumId,
        Guid UserId,
        string Title,
        string Description,
        string? ImageUrl,
        IFormFile Image
    ) : IRequest<Guid>;


}