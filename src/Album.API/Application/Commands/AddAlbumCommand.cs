using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Album.API.Application.Commands
{
    public record AddAlbumCommand
    (
        Guid ProducerId,
        string Title,
        string Description,
        decimal Price,
        string ImageUrl,
        IFormFile image
    ) : IRequest<Guid>;
}