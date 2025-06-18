using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music.API.Domain.Aggregates;

namespace Music.API.Domain.Interfaces
{
    public interface ISingleMusicRepository
    {
        Task AddAsync(SingleMusic music);
        Task UpdateAsync(SingleMusic music);
    }
}