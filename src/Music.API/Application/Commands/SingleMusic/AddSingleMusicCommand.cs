using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Music.API.Application.Commands.SingleMusic
{
    public record AddSingleMusicCommand
    (
        Guid UserId,
        string Title,
        string Description,
        string? ImageUrl,
        IFormFile Image
    ) : IRequest<Guid>;
}
