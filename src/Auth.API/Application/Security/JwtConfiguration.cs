using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.API.Domain.Interfaces;
using Auth.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace Auth.API.Application.Security
{
    public class JwtConfiguration : IJwtConfiguration
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepository _userRepository;

        public JwtConfiguration(IUserRepository userRepository, JwtSettings jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }

        public async Task<string> GenerateTokenAsync(string email)
        {
            var client = await _userRepository.GetByEmailAsync(email);
            if (client == null) throw new InvalidOperationException("User not found.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
                new Claim(ClaimTypes.Email, client.Email),
                new Claim(ClaimTypes.Role, client.Role.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JWT_KEY));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.JWT_DurationInMinutes);

            var token = new JwtSecurityToken
            (
                issuer: _jwtSettings.JWT_ISSUER,
                audience: _jwtSettings.JWT_AUDIENCE,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
