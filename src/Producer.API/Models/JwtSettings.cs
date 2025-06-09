using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.API.Models
{
    public class JwtSettings
    {
        public string JWT_KEY { get; set; }
        public string JWT_ISSUER { get; set; }
        public string JWT_AUDIENCE { get; set; }
        public int JWT_DurationInMinutes { get; set; }
    }
}