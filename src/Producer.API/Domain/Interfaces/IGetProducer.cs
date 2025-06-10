using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Producer.API.API.DTOs;

namespace Producer.API.Domain.Interfaces
{
    public interface IGetProducer
    {
        Task<CurrentProducerDto> GetCurrentProducerAsync();
    }
}