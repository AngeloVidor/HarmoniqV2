using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Producer.API.API.DTOs;
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

        public async Task<CurrentProducerDto> GetCurrentProducerAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext?.Items["userId"] as Guid?;

            if (user == null || user == Guid.Empty)
                throw new UnauthorizedAccessException("User ID not found in context");

            var currentProducer = await _getProducerRepository.GetProducerByUserIdAsync(user.Value);
            return new CurrentProducerDto
            {
                Id = currentProducer.Id,
                UserId = currentProducer.UserId,
                Name = currentProducer.Name,
                Description = currentProducer.Description,
                Country = currentProducer.Country,
                ImageUrl = currentProducer.ImageUrl
            };
        }
    }
}