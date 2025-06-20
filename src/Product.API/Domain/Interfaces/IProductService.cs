using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Domain.Interfaces
{
    public interface IProductService
    {
        Task<(string productId, string priceId)> CreateProductAsync(Domain.Aggregates.Product product);
    }
}