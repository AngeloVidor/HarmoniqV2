using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.API.Domain.Interfaces
{
    public interface IConsumerRepository
    {
        Task<bool> AddAsync(Domain.Aggregates.Consumer consumer);
    }
}