using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Producer.API.API.DTOs;

namespace Producer.API.Application.Queries
{
    public record GetProducersQuery() : IRequest<IEnumerable<ProducersDto>>;

}