using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Domain.Exceptions
{
    public class ProducerNotFoundException : Exception
    {
        public ProducerNotFoundException() : base("Producer not found for this user.") { }

    }
}