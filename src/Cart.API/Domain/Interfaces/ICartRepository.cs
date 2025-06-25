using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task AddAsync(Aggregates.Cart cart);
    }
}