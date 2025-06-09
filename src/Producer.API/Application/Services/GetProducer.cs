using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Producer.API.Domain.Interfaces;
using Producer.API.Infrastructure.Repositories;

namespace Producer.API.Application.Services
{
    public class GetProducer : IGetProducer
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProducerRepository _producerRepository;

        public GetProducer(IHttpContextAccessor httpContextAccessor, IProducerRepository producerRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _producerRepository = producerRepository;
        }

        public async Task<Domain.Aggregates.Producer> GetCurrentProducerAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext?.Items["userId"] as Guid?;

            if (user == null || user == Guid.Empty)
                throw new UnauthorizedAccessException("User ID not found in context");

            return await _producerRepository.GetByIdAsync(user.Value);
        }
    }
}