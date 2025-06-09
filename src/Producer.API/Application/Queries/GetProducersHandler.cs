using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Producer.API.API.DTOs;
using Producer.API.Domain.Interfaces;

namespace Producer.API.Application.Queries
{
    public class GetProducersHandler : IRequestHandler<GetProducersQuery, IEnumerable<ProducersDto>>
    {
        private readonly IGetProducerRepoitory _getProducerRepoitory;

        public GetProducersHandler(IGetProducerRepoitory getProducerRepoitory)
        {
            _getProducerRepoitory = getProducerRepoitory;
        }

        public async Task<IEnumerable<ProducersDto>> Handle(GetProducersQuery request, CancellationToken cancellationToken)
        {
            var producers = await _getProducerRepoitory.GetProducersAsync();

            var result = producers.Select(p => new ProducersDto
            {
                Name = p.Name,
                Description = p.Description,
                Country = p.Country,
                ImageUrl = p.ImageUrl
            });

            return result;

        }
    }
}