using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Domain.Aggregates
{
    public class Cart
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid ConsumerId { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public decimal TotalAmount { get; private set; }
        public List<CartItem> Items { get; private set; }

        public Cart(Guid consumerId)
        {
            Id = Guid.NewGuid();
            ConsumerId = consumerId;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            TotalAmount = 0;
            Items = new List<CartItem>();
        }
    }
}