using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.API.Domain.Interfaces
{
    public interface IProducerRepository
    {
        Task AddAsync(Aggregates.Producer producer);
        Task UpdateAsync(Aggregates.Producer producer);
        Task<Aggregates.Producer> GetByIdAsync(Guid userId);
    }
}