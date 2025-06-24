using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.API.Domain.Interfaces
{
    public interface IConsumerReadRepository
    {
        Task<Domain.Aggregates.Consumer> GetConsumerByUserIdAsync(Guid userId);
    }
}