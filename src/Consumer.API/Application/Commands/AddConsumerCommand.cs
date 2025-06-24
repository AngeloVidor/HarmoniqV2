using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Consumer.API.Application.Commands
{
    public record AddConsumerCommand(string Name, string Description, string? ImageUrl, Guid UserId) : IRequest<bool>;


}