using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.API.Domain.Exceptions
{
    public class ConsumerAlreadyExistsException : Exception
    {
        public ConsumerAlreadyExistsException() : base("A consumer with the same user ID already exists.") { }
    }

}