using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Identity.Client;

namespace Producer.API.Application.Commands
{
    public record AddProducerCommand
    (
        Guid UserId,
        string Name,
        string Description,
        string Country,
        string? ImageUrl,
        IFormFile ImageFile
    ) : IRequest<Guid>;


}