using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Domain.Snapshots;

namespace Cart.API.Domain.Interfaces
{
    public interface IConsumerReadRepository
    {
        Task<Consumer> GetByIdAsync(Guid consumerId);
        Task<Consumer> GetConsumerByUserIdAsync(Guid userId);
    }
}