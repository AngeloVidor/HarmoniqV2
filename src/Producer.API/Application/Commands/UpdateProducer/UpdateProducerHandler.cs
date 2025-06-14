using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Producer.API.Domain.Interfaces;

namespace Producer.API.Application.Commands.UpdateProducer
{
    public class UpdateProducerHandler : IRequestHandler<UpdateProducerCommand, bool>
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IGetProducerRepoitory _getProducerRepository;

        public UpdateProducerHandler(IProducerRepository producerRepository, IGetProducerRepoitory getProducerRepository)
        {
            _producerRepository = producerRepository;
            _getProducerRepository = getProducerRepository;
        }

        public async Task<bool> Handle(UpdateProducerCommand request, CancellationToken cancellationToken)
        {
            var producer = await _getProducerRepository.GetProducerByUserIdAsync(request.UserId);
            if (producer == null)
            {
                return false;
            }

            try
            {
                producer.Update(request.Name, request.Description, request.Country);
                await _producerRepository.UpdateAsync(producer);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating producer: {ex.Message}");
                return false;
            }
        }
    }
}