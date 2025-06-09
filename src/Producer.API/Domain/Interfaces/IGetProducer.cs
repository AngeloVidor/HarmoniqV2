using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.API.Domain.Interfaces
{
    public interface IGetProducer
    {
        Task<Aggregates.Producer> GetCurrentProducerAsync();
    }
}