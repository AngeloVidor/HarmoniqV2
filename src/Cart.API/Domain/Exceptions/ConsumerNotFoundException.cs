using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Domain.Exceptions
{
    public class ConsumerNotFoundException : Exception
    {
        public ConsumerNotFoundException() : base("Consumer not found.")
        {
        }
    }
}