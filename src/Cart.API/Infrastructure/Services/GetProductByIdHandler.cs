using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Application.Commands;
using Cart.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cart.API.Infrastructure.Services
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdCommand, Product>
    {
        private readonly HttpClient _httpClient;


        public GetProductByIdHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Product> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            string url = $"http://localhost:5072/api/Product/v2/id?ProductId={request.ProductId}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var product = await response.Content.ReadFromJsonAsync<Product>();
            if (product == null)
                throw new InvalidOperationException();

            return product;
        }
    }
}