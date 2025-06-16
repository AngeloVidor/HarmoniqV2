using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music.API.Domain.Entities;

namespace Music.API.Domain.Interfaces
{
    public interface ISnapshotRepository
    {
        Task SaveAsync(Producer producer);
    }
}