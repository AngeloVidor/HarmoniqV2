using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Snapshots;

namespace Album.API.Domain.Interfaces
{
    public interface IProducerService 
    {
        Task<ProducerSnap> GetProducerByUserId(Guid userId);
    }
}