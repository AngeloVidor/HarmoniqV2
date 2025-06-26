using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Domain.Exceptions;
using Cart.API.Domain.Interfaces;
using MediatR;

namespace Cart.API.Application.Commands
{
    public class AddCartHandler : IRequestHandler<AddCartCommand, Guid>
    {
        private readonly IConsumerReaderRepository _consumerReaderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartReaderRepository _cartReaderRepository;

        public AddCartHandler(IConsumerReaderRepository consumerReaderRepository, ICartRepository cartRepository, ICartReaderRepository cartReaderRepository)
        {
            _consumerReaderRepository = consumerReaderRepository;
            _cartRepository = cartRepository;
            _cartReaderRepository = cartReaderRepository;
        }

        public async Task<Guid> Handle(AddCartCommand request, CancellationToken cancellationToken)
        {
            var consumer = await _consumerReaderRepository.GetConsumerByUserIdAsync(request.UserId);
            if (consumer == null)
                throw new ConsumerNotFoundException();

            var existingCart = await _cartReaderRepository.GetCartByConsumerIdAsync(consumer.ConsumerId);
            if (existingCart != null && existingCart.IsActive)
                throw new CannotCreateCartWhenAlreadyActiveException();

            var cart = new Domain.Aggregates.Cart(consumer.ConsumerId);

            await _cartRepository.AddAsync(cart);
            return cart.Id;
        }
    }
}