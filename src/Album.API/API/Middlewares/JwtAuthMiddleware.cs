using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Album.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace Album.API.API.Middlewares
{
    public class JwtAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;

        public JwtAuthMiddleware(RequestDelegate next, JwtSettings jwtConfig)
        {
            _next = next;
            _jwtSettings = jwtConfig;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            List<string> PublicRoutes = new List<string>
            {

            };

            if (PublicRoutes.Contains(context.Request.Path.Value, StringComparer.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            var tokenAccess = context.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];
            if (string.IsNullOrEmpty(tokenAccess)) throw new Exception("Token not provided");


            var isValid = ValidateToken(tokenAccess);
            if (!isValid) throw new Exception("Invalid token");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(tokenAccess) as JwtSecurityToken;
            var userClaims = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userClaims == null) throw new Exception("UserId not found in claims");

            if (!Guid.TryParse(userClaims.Value, out var userId))
                throw new Exception("UserId in token is not a valid GUID");

            context.Items["userId"] = userId;

            await _next(context);
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var secretKey = _jwtSettings.JWT_KEY;
                if (string.IsNullOrEmpty(secretKey)) throw new Exception("JWT Secret Key is not configured");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _jwtSettings.JWT_ISSUER,
                    ValidAudience = _jwtSettings.JWT_AUDIENCE,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = handler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                if (!(validatedToken is JwtSecurityToken jwtSecurityToken)) throw new Exception("Invalid token");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Token validation failed: {ex.Message}");
            }
        }
    }
}