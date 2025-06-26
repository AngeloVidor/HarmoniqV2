using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Domain.Aggregates;
using Cart.API.Domain.Exceptions;
using Cart.API.Domain.Interfaces;
using MediatR;

namespace Cart.API.Application.Commands
{
    public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, bool>
    {
        private readonly IConsumerReaderRepository _consumerReader;
        private readonly ICartReaderRepository _cartReader;
        private readonly IMediator _mediator;
        private readonly ICartItemRepository _itemRepository;

        public AddCartItemHandler(IConsumerReaderRepository consumerReader, ICartReaderRepository cartReader, IMediator mediator, ICartItemRepository itemRepository)
        {
            _consumerReader = consumerReader;
            _cartReader = cartReader;
            _mediator = mediator;
            _itemRepository = itemRepository;
        }

        public async Task<bool> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var consumer = await _consumerReader.GetConsumerByUserIdAsync(request.UserId);
            if (consumer is null)
                throw new ConsumerNotFoundException();

            var cart = await _cartReader.GetCartByConsumerIdAsync(consumer.ConsumerId);
            if (cart is null)
                throw new CartNotFoundException();

            if (!cart.IsActive)
                throw new InactiveCartException();

            //need to make a rest call in the Product.API to check if the productId is valid
            var product = await _mediator.Send(new GetProductByIdCommand(request.ProductId), cancellationToken);
            Console.WriteLine(product.Name);
            Console.WriteLine(product.Name);
            Console.WriteLine(product.Name);
            Console.WriteLine(product.Name);
            Console.WriteLine(product.Name);

            if (product is null) throw new ProductNotFoundException();

            var item = new CartItem(cart.Id, product.Id, product.Name, product.Price);

            await _itemRepository.AddAsync(item);

            return true;
        }
    }
}
