using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Producer.API.Application.Queries
{
    public record GetProducersQuery() : IRequest<IEnumerable<Domain.Aggregates.Producer>>;

}