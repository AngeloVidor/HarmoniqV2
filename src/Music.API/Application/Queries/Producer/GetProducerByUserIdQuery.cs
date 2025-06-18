using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Music.API.Domain.Entities;

namespace Music.API.Application.Queries
{
    public record GetProducerByUserIdQuery(Guid userId) : IRequest<Producer>;

}