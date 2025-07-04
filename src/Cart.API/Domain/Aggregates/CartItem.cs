using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Domain.Aggregates
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid CartId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public DateTime AddedAt { get; private set; }

        public CartItem(Guid cartId, Guid productId, string productName, decimal price)
        {
            CartId = cartId;
            ProductId = productId;
            ProductName = productName;
            Price = price;
            AddedAt = DateTime.UtcNow;
        }
    }
}