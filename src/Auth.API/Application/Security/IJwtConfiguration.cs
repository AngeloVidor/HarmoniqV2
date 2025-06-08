using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Application.Security
{
    public interface IJwtConfiguration
    {
        Task<string> GenerateTokenAsync(string email);
    }
}