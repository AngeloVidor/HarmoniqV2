using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.API.Domain.Aggregates;

namespace Product.API.Domain.Interfaces
{
    public interface IProductReaderRepository
    {
        Task<Aggregates.Product> GetProductByIdAsync(Guid productId);
    }
}