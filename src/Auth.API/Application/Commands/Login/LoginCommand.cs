using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Auth.API.Application.Commands.Login
{
    public record LoginCommand
    (
        string Email,
        string Password
        
    ) : IRequest<bool>;
}