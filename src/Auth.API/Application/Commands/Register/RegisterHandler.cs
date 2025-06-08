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
            var userEmail = await _userRepository.GetByEmailAsync(request.Email);
            if (userEmail != null)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            var hashedPassword = User.HashPassword(request.Password);

            var user = new User
            (
                username: request.Username,
                hashedPassword: hashedPassword,
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
