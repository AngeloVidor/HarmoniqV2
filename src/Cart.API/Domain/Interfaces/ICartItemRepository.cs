using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Domain.Aggregates;

namespace Cart.API.Domain.Interfaces
{
    public interface ICartItemRepository
    {
        Task AddAsync(CartItem item);
    }
}