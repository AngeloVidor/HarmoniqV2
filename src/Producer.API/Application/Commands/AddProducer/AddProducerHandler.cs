using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Producer.API.Domain.Interfaces;

namespace Producer.API.Application.Commands
{
    public class AddProducerHandler : IRequestHandler<AddProducerCommand, Guid>
    {
        private readonly IProducerRepository _producerRepository;

        public AddProducerHandler(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        public async Task<Guid> Handle(AddProducerCommand request, CancellationToken cancellationToken)
        {
            var producer = new Domain.Aggregates.Producer(request.Name, request.Description, request.Country, request.UserId);
            await _producerRepository.AddAsync(producer);
            return producer.Id;
        }
    }
}