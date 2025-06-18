using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using MediatR;

namespace Music.API.Application.Commands.SingleMusic
{
    public record UpdateSingleMusicCommand(Guid Id, Guid UserId, string? ImageUrl, IFormFile Image, string Title, string Description) : IRequest<bool>;
}