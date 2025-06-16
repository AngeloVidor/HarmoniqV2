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
        Guid? AlbumId,
        Guid UserId,
        bool IsPartOfAlbum,
        string Title,
        string Description,
        string? ImageUrl
    ) : IRequest<Guid>;


}