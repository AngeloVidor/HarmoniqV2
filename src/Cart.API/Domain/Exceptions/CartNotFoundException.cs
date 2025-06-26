using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Domain.Exceptions
{
    public class CartNotFoundException : Exception
    {
        public CartNotFoundException() : base("Cart not found")
        {
        }
    }
}