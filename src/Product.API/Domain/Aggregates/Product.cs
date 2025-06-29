using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Domain.Aggregates
{
    public class Product
    {
        [Key]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public decimal Price { get; private set; }
        public string StripeProductId { get; private set; }
        public string StripePriceId { get; private set; }

        public Product(string name, string description, decimal price, string stripeProductId = null, string stripePriceId = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            StripeProductId = stripeProductId;
            StripePriceId = stripePriceId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}