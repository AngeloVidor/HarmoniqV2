using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Domain.Snapshots;

namespace Cart.API.Domain.Interfaces
{
    public interface IConsumerRepository
    {
        Task AddAsync(Consumer consumer);
    }
}