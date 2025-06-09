using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Producer.API.Application.Commands.UpdateProducer
{
    public record UpdateProducerCommand
    (
        Guid UserId,
        Guid ProducerId,
        string Name,
        string Description,
        string Country
    ) : IRequest<bool>;
}