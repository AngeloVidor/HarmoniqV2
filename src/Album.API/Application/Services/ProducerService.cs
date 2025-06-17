using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.API.Domain.Interfaces;
using Album.API.Domain.Snapshots;

namespace Album.API.Application.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerService(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        public async Task<ProducerSnap> GetProducerByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId), "userId cannot be Guid.Empty");
            Console.WriteLine(userId);
            Console.WriteLine(userId);
            Console.WriteLine(userId);
            Console.WriteLine(userId);

            return await _producerRepository.GetProducerByUserId(userId);
        }
    }
}