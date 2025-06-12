using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Interfaces;
using Album.API.Domain.Snapshots;

namespace Album.API.Application.Services
{
    public class GetProducerService : IGetProducerService
    {
        private readonly IGetProducerRepository _getProducerRepository;

        public GetProducerService(IGetProducerRepository getProducerRepository)
        {
            _getProducerRepository = getProducerRepository;
        }

        public async Task<ProducerSnap> GetProducerByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId), "userId cannot be Guid.Empty");
                
            return await _getProducerRepository.GetProducerByUserId(userId);
        }
    }
}