using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.API.Domain.Exceptions;
using Consumer.API.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Consumer.API.Application.Commands
{
    public class AddConsumerHandler : IRequestHandler<AddConsumerCommand, bool>
    {
        private readonly IConsumerRepository _consumerRepository;
        private readonly IConsumerReadRepository _readRepository;

        public AddConsumerHandler(IConsumerRepository consumerRepository, IConsumerReadRepository readRepository)
        {
            _consumerRepository = consumerRepository;
            _readRepository = readRepository;
        }

        public async Task<bool> Handle(AddConsumerCommand request, CancellationToken cancellationToken)
        {
            var existingConsumer = await _readRepository.GetConsumerByUserIdAsync(request.UserId);
            if (existingConsumer != null)
                throw new ConsumerAlreadyExistsException();

            var consumer = new Domain.Aggregates.Consumer(request.Name, request.Description, request.ImageUrl, request.UserId);

            await _consumerRepository.AddAsync(consumer);
            return true;
        }
    }
}