using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.API.Domain.Interfaces
{
    public interface IGetProducerRepoitory
    {
        Task<IEnumerable<Domain.Aggregates.Producer>> GetProducersAsync();
        Task<Domain.Aggregates.Producer> GetProducerByUserIdAsync(Guid id);
    }
}