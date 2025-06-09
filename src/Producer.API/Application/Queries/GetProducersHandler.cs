using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Producer.API.Domain.Interfaces;

namespace Producer.API.Application.Queries
{
    public class GetProducersHandler : IRequestHandler<GetProducersQuery, IEnumerable<Domain.Aggregates.Producer>>
    {
        private readonly IGetProducerRepoitory _getProducerRepoitory;

        public GetProducersHandler(IGetProducerRepoitory getProducerRepoitory)
        {
            _getProducerRepoitory = getProducerRepoitory;
        }

        public async Task<IEnumerable<Domain.Aggregates.Producer>> Handle(GetProducersQuery request, CancellationToken cancellationToken)
        {
            return await _getProducerRepoitory.GetProducersAsync();

        }
    }
}