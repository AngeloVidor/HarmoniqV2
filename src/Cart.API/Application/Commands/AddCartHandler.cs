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
        private readonly IConsumerReadRepository _consumerReadRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartReaderRepository _cartReadeRepository;

        public AddCartHandler(IConsumerReadRepository consumerReadRepository, ICartRepository cartRepository, ICartReaderRepository cartReadeRepository)
        {
            _consumerReadRepository = consumerReadRepository;
            _cartRepository = cartRepository;
            _cartReadeRepository = cartReadeRepository;
        }

        public async Task<Guid> Handle(AddCartCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.UserId);

            var consumer = await _consumerReadRepository.GetConsumerByUserIdAsync(request.UserId);
            if (consumer == null)
                throw new ConsumerNotFoundException();

            var existingCart = await _cartReadeRepository.GetCartByConsumerIdAsync(consumer.ConsumerId);
            if (existingCart != null && existingCart.IsActive)
                throw new CannotCreateCartWhenAlreadyActiveException();
                
            var cart = new Domain.Aggregates.Cart(consumer.ConsumerId);

            await _cartRepository.AddAsync(cart);
            return cart.Id;
        }
    }
}