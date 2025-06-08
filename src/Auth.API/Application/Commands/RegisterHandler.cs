using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Domain.Aggregates;
using Auth.API.Domain.Interfaces;
using MediatR;

namespace Auth.API.Application.Commands
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public RegisterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            (
                username: request.Username,
                password: request.Password,
                email: request.Email,
                firstName: request.FirstName,
                lastName: request.LastName,
                role: request.Role
            );

            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}
