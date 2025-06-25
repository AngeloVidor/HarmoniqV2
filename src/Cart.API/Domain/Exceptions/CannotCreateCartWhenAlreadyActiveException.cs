using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Domain.Exceptions
{
    public class CannotCreateCartWhenAlreadyActiveException : Exception
    {
        public CannotCreateCartWhenAlreadyActiveException() : base("There is already an active cart for this consumer.")
        {
        }
    }
}