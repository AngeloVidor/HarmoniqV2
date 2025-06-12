using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Snapshots;

namespace Album.API.Domain.Interfaces
{
    public interface IProducerSnapshotRepository
    {
        Task SaveAsync(ProducerSnap snapshot);
    }
}