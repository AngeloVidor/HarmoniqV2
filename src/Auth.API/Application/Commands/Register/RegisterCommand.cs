using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Domain.ValueObjects;
using MediatR;

namespace Auth.API.Application.Commands
{
    public record RegisterCommand
    (
        string Username,
        string Password,
        string FirstName,
        string LastName,
        string Email,
        Roles Role
    ) : IRequest<Guid>;
}