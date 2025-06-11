using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Producer.API.Domain.Interfaces;
using Producer.API.Infrastructure.Messaging;

namespace Producer.API.Application.Commands
{
    public class AddProducerHandler : IRequestHandler<AddProducerCommand, Guid>
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IGetProducerRepoitory _getProducerRepoitory;
        private readonly IProducerCreatedEvent @event;

        public AddProducerHandler(IProducerRepository producerRepository, IGetProducerRepoitory getProducerRepoitory, IProducerCreatedEvent @event)
        {
            _producerRepository = producerRepository;
            _getProducerRepoitory = getProducerRepoitory;
            this.@event = @event;
        }

        public async Task<Guid> Handle(AddProducerCommand request, CancellationToken cancellationToken)
        {
            var profileAlreadyExists = await _getProducerRepoitory.GetProducerByUserIdAsync(request.UserId);
            if (profileAlreadyExists != null)
                throw new Exception("This user already created a producer profile");

            var producer = new Domain.Aggregates.Producer(request.Name, request.Description, request.Country, request.UserId, request.ImageUrl);
            var persistence = await _producerRepository.AddAsync(producer);
            if (persistence)
            {
                await @event.Publish(producer.Id, producer.UserId, producer.Name);
            }
            return producer.Id;
        }
    }
}