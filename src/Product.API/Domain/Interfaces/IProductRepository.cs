using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task SaveAsync(Domain.Aggregates.Product product);
    }
}