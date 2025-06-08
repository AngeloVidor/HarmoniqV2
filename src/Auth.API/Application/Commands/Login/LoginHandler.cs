using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Domain.Interfaces;
using BCrypt.Net;
using MediatR;

namespace Auth.API.Application.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public LoginHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var client = await _userRepository.GetByEmailAsync(request.Email);
            Console.WriteLine($"Received password: {request.Password}");

            if (client == null || !BCrypt.Net.BCrypt.Verify(request.Password, client.Password))
            {
                Console.WriteLine(client.Password);
                throw new Exception("Invalid email or password");
            }
            return true;


        }
    }
}