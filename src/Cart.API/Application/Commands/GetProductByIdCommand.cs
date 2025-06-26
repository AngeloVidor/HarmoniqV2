using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Models;
using MediatR;

namespace Cart.API.Application.Commands
{
    public record GetProductByIdCommand(Guid ProductId) : IRequest<Product>;
}