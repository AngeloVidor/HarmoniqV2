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
        private readonly IGetProducerRepoitory _getProducerRepository;

        public GetProducer(IGetProducerRepoitory getProducerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _getProducerRepository = getProducerRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Domain.Aggregates.Producer> GetCurrentProducerAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext?.Items["userId"] as Guid?;
 
            if (user == null || user == Guid.Empty)
                throw new UnauthorizedAccessException("User ID not found in context");

            return await _getProducerRepository.GetProducerByUserIdAsync(user.Value);
        }
    }
}