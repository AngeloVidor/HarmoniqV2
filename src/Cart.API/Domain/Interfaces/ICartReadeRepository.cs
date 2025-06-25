using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Domain.Interfaces
{
    public interface ICartReaderRepository
    {
        Task<Aggregates.Cart> GetCartByConsumerIdAsync(Guid consumerId);
    }
}