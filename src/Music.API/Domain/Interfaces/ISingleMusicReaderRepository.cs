using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music.API.Domain.Aggregates;

namespace Music.API.Domain.Interfaces
{
    public interface ISingleMusicReaderRepository
    {
        Task<SingleMusic> GetSingleMusicByIdAsync(Guid id);
        Task<SingleMusic> GetProducerSingleMusicById(Guid producerId, Guid musicId);
    }
}