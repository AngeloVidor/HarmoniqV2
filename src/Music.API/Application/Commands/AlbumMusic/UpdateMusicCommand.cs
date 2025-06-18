using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Music.API.Application.Commands.AlbumMusic
{
    public record UpdateMusicCommand(Guid id, Guid userId, Guid albumId, string? imageUrl, IFormFile Image, string title, string description) : IRequest<bool> { }
}