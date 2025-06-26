using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.API.Domain.Exceptions;
using Product.API.Domain.Interfaces;

namespace Product.API.Application.Queries
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Domain.Aggregates.Product>
    {
        private readonly IProductReaderRepository _productReader;

        public GetProductByIdHandler(IProductReaderRepository productReader)
        {
            _productReader = productReader;
        }

        public async Task<Domain.Aggregates.Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productReader.GetProductByIdAsync(request.ProductId);
            if (product == null)
                throw new ProductNotFoundException();

            return product;
        }
    }
}