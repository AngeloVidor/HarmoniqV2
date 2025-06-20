using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Domain.ValueObjects
{
    public class StripeProductResult
    {
        public Domain.Aggregates.Product Product { get; set; }
        public string StripeProductId { get; set; }
        public string StripePriceId { get; set; }
    }
}