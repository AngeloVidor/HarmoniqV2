using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Domain.Snapshots
{
    public class Consumer
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid ConsumerId { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; }

        public Consumer(Guid consumerId, Guid userId, string name)
        {
            Id = Guid.NewGuid();
            ConsumerId = consumerId;
            UserId = userId;
            Name = name;
        }
    }
}