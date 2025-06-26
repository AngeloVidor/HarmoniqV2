using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Product.API.Application.Queries
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<Domain.Aggregates.Product>;
}